using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Consulta
    {
        /// <summary>
        /// lista con todos los cursos a retornar
        /// </summary>
        public class ListaCursos : IRequest<List<CursoDTO>> 
        { }

        /// <summary>
        /// lo que devuelve la ejecucion de esta clase
        /// </summary>
        public class Manejador : IRequestHandler<ListaCursos, List<CursoDTO>>
        {
            private readonly CursosOnlineContext _context;
            private readonly IMapper _mapper;

            public Manejador(CursosOnlineContext context, IMapper mapper)
            {
                this._context = context;
                this._mapper = mapper;
            }

            public async Task<List<CursoDTO>> Handle(ListaCursos request, CancellationToken cancellationToken)
            {
                // devolver los Cursos junto a los Comentarios, el Precio, y los Instructores relacionados a un curso
                var cursos = await this._context.Curso
                    .Include(x => x.ComentarioLista)
                    .Include(x => x.PrecioPromocion)
                    .Include(x => x.InstructoresLinks)
                    .ThenInclude(x => x.Instructor).ToListAsync();

                // recibe el tipo de dato origen, y el segundo es para saber en que se convertira.
                var cursosDTO = this._mapper.Map<List<Curso>, List<CursoDTO>>(cursos);

                // debe hcerse el mapping para obtener los instructores desde ClaseDTO
                return cursosDTO;
            }
        }

    }
}
