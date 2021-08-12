using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Comentario
    {
        public Guid ComentarioId { get; set; }
        public string Alumno { get; set; }
        public int Puntaje { get; set; }
        public string ComentarioTexto { get; set; }

        /// <summary>
        /// llave foranea para relacionar las tablas
        /// </summary>
        public Guid CursoId { get; set; }

        public DateTime? FechaCreacion { get; set; }

        /// <summary>
        ///  metodo pada obtener el curso relacinado
        /// </summary>
        public Curso Curso { get; set; }
    }
}
