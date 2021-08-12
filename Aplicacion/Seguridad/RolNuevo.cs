using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad
{
    // Seguridad a nivel avanzado
    public class RolNuevo
    {
        // los roles se agregan a los ususarios
        public class Ejecuta : IRequest
        {
            public string Nombre { get; set; }
        }

        public class ValidaEjecuta : AbstractValidator<Ejecuta>
        {
            public ValidaEjecuta()
            {
                RuleFor(x => x.Nombre).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly RoleManager<IdentityRole> _roleManager;
            public Manejador(RoleManager<IdentityRole> rolManager)
            {
                this._roleManager = rolManager;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var role = await this._roleManager.FindByNameAsync(request.Nombre);

                // se verifica ue el rol a agregar no exista
                if (role != null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "El rol ya existe" });
                }

                // envio de los datos para crear un nuevo Rol si el rol no existe.
                var resultado = await this._roleManager.CreateAsync(new IdentityRole(request.Nombre));
                if(resultado.Succeeded)
                {
                    return Unit.Value;
                }

                // algo sucedio y no se pudo enviar los datos del rol
                throw new Exception("No se pudo guardar el rol"); 
            }
        }

    }
}
