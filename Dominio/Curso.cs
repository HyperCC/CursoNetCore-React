using System.Collections.Generic;
using System;

namespace Dominio
{
    public class Curso
    {
        public Guid CursoId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public byte[] FotoPortada { get; set; }

        /// <summary>
        /// metodo para obtener el objeto precio relacionado
        /// </summary>
        public Precio PrecioPromocion { get; set; }

        public DateTime? FechaCreacion { get; set; }


        /// <summary>
        /// metodo para obtener todos los comentarios relacionados a este curso
        /// </summary>
        public ICollection<Comentario> ComentarioLista { get; set; }

        /// <summary>
        /// metodo para obtener todos los Instructores relacionados a este curso
        /// </summary>
        public ICollection<CursoInstructor> InstructoresLinks { get; set; }


    }
}
