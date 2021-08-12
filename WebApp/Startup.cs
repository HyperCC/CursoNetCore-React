using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Persistencia;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Aplicacion.Cursos;
using Aplicacion.Contratos;
using FluentValidation.AspNetCore;
using WebApp.Middleware;
using Dominio;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Authentication;
using Seguridad.TokenSeguridad;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Persistencia.DapperConexion;
using Persistencia.DapperConexion.Instructor;
using Microsoft.OpenApi.Models;
using Persistencia.Paginacion;

namespace WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // configuracion para agregar el context al servicio web
            services.AddDbContext<CursosOnlineContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            // permite ue Dapper pueda leer la cadena de conexion para los procedimientos almacenados
            services.AddOptions();
            // configurar los procedimientos almacenados para la DB
            services.Configure<ConexionConfiguracion>(Configuration.GetSection("ConnectionStrings"));

            // configura el mediador para devolver todos los cursos
            services.AddMediatR(typeof(Consulta.Manejador).Assembly);

            services.AddControllers(opt =>
            {
                // agregar [Authorize] a todos los controladores para validar el funcionamiento con tokens de usuario
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                opt.Filters.Add(new AuthorizeFilter(policy));
            })
                .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>());

            // configuracion de core identity para el acceso por logins
            var builder = services.AddIdentityCore<Usuario>();
            var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);

            // instanciar el uso de roles de usuario con los datos pre contrstruidos de IdentityRole
            identityBuilder.AddRoles<IdentityRole>();
            // Claims comunicando las entidades Usuario y IdentityRole
            identityBuilder.AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<Usuario, IdentityRole>>();

            // guardado de los datos para los usuarios ue ingresen a la aplicacion 
            identityBuilder.AddEntityFrameworkStores<CursosOnlineContext>();
            identityBuilder.AddSignInManager<SignInManager<Usuario>>();

            // configuracion de los datos de prueba para las migraciones
            services.TryAddSingleton<ISystemClock, SystemClock>();

            //creacioon de la llave para la validacion de los controladores con seguridad
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Mi palabra secreta"));
            //habilitar la autenticacion por tokens para obtener datos desde la API
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    // cualquier request del cliente debe ser validado por el proyecto
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    // personas por IP que pueden acceder al servicio
                    ValidateAudience = false,
                    // envio del token falso, ya que no hay seleccion de cliente especificos en ValidateAudience
                    ValidateIssuer = false
                };
            });

            // injeccion de la libreria de seguridad y la interface en applicacion.contratos 
            // y comenzar interfaces reconocibles parar logica de negocio
            services.AddScoped<IJwtGenerador, JwtGenerador>();

            // dar a concer por el webApp la clase para reconocer l usuario en sesion acltualmente.
            services.AddScoped<IUsuarioSesion, UsuarioSesion>();

            // Inicializar injeccion del mapping de instructores en clasesDTO
            services.AddAutoMapper(typeof(Consulta.Manejador));

            // instanciar los procedimientos almacenados como clases en Persistencia
            services.AddTransient<IFactoryConnection, FactoryConnection>();
            // instanciar operaciones de DJ como tal para los procedimientos alamcenados
            services.AddScoped<IInstructor, InstructorRepositorio>();

            // instancia la paginacion 
            services.AddScoped<IPaginacion, PaginacionRepositorio>();

            // confguracion basica del Swagger - genera documentacion para una API con interfaz grafica
            services.AddSwaggerGen(c =>
            {
                // se manda la version y la informacion de la API
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Services para mentenimiento de Cursos",
                    Version = "v1"
                });
                // filtro para no tener conflictos con los esquemas de los Endpoints
                // los metodos Ejecuta chocarian unos otros si no se agrega "FullName", de otro se usa el namespace para separar todo.
                c.CustomSchemaIds(c => c.FullName);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // nuevo midleware con los errores personalizados
            app.UseMiddleware<ManejadorErrorMiddleware>();

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            // indicar la inicializacion de la validacion para los resultados de los request de clientes
            app.UseAuthentication();

            // declarar uso del Swagger - SE ACCEDE MEDIANTE http://host:5000/swagger/index.html - 
            app.UseSwagger();
            // configuracion de la interfaz grafica del Swagger
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cursos Online v1");
            });

            // declarar uso de los endpoint para los controladores 
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
