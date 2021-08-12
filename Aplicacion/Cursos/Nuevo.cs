using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using FluentValidation;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    // ingresar un curso a la DB
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
            public DateTime? FechaPublicacion { get; set; }

            // para la relacion N -> N entre Curso e Instructor
            public List<Guid> ListaInstructor { get; set; }

            // para agregar un precio al momento de crear el Curso nuevo
            public decimal Precio { get; set; }
            public decimal Promocion { get; set; }
        }

        // validaciones de los datos 
        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Titulo).NotEmpty();
                RuleFor(x => x.Descripcion).NotEmpty();
                RuleFor(x => x.FechaPublicacion).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly CursosOnlineContext _context;
            public Manejador(CursosOnlineContext context)
            {
                this._context = context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                Guid _cursoId = Guid.NewGuid();

                // crear el nuevo curso
                var curso = new Curso
                {
                    CursoId = _cursoId,
                    Titulo = request.Titulo,
                    Descripcion = request.Descripcion,
                    FechaPublicacion = request.FechaPublicacion,
                    FechaCreacion = DateTime.UtcNow
                };
                
                // agregar el curso a la DB
                this._context.Add(curso);

                // agregar los CursoInstructor relacionados a cada modelo Curso
                if (request.ListaInstructor != null)
                    foreach (var id in request.ListaInstructor)
                    {
                        var cursoInstructor = new CursoInstructor
                        {
                            CursoId = _cursoId,
                            InstructorId = id
                        };

                        this._context.CursoInstructor.Add(cursoInstructor);
                    }

                var precioEntidad = new Precio
                {
                    CursoId = _cursoId,
                    PrecioActual = request.Precio,
                    Promocion = request.Promocion,
                    PrecioId = Guid.NewGuid()
                };
                this._context.Precio.Add(precioEntidad);

                // operation devuelve a lista de operaciones realizadas por la DB, un numero por cada operacion
                var operation = await this._context.SaveChangesAsync();
                if (operation > 0)
                    return Unit.Value;

                throw new Exception("No se pudo insertar el nuevo Curso");
            }
        }

    }
}
