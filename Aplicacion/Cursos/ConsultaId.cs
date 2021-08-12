using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Dominio;
using System.Threading.Tasks;
using System.Threading;
using Persistencia;
using Aplicacion.ManejadorError;
using System.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Cursos
{
    public class ConsultaId
    {
        public class CursoUnico : IRequest<CursoDTO>
        {
            public Guid Id { get; set; }
        }

        public class Manejador : IRequestHandler<CursoUnico, CursoDTO>
        {
            private readonly CursosOnlineContext _context;
            private readonly IMapper _mapper;

            public Manejador(CursosOnlineContext context, IMapper mapper)
            {
                this._context = context;
                this._mapper = mapper;
            }

            public async Task<CursoDTO> Handle(CursoUnico request, CancellationToken cancellationToken)
            {
                // encontrar un curso y para tal curso obtener todos los instructores relacionados  
                var curso = await this._context.Curso
                    .Include(x => x.ComentarioLista)
                    .Include(x => x.PrecioPromocion)
                    .Include(x => x.InstructoresLinks)
                    .ThenInclude(y => y.Instructor)
                    .FirstOrDefaultAsync(a => a.CursoId == request.Id);

                if (curso == null)
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontro el curso buscado" });

                // mapeo desde modelo curso a cursoDTO
                var cursoDto = this._mapper.Map<Curso, CursoDTO>(curso);

                return cursoDto;
            }
        }
    }
}
