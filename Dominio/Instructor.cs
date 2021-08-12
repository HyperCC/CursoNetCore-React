using System;
using System.Collections.Generic;

namespace Dominio
{
    public class Instructor
    {
        public Guid InstructorId { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Grado { get; set; }
        public byte[] FotoPerfil { get; set; }

        public DateTime? FechaCreacion { get; set; }


        /// <summary>
        /// todos los cursos relacionados a este instructor
        /// </summary>
        public ICollection<CursoInstructor> CursoLink { get; set; }
    }
}
