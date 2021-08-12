using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class CursoInstructor
    {
        /// <summary>
        /// llaves primarias y foraneas para esta tabla relacion n-n
        /// </summary>
        public Guid InstructorId { get; set; }
        public Guid CursoId { get; set; }

        /// <summary>
        /// metodos para obtener los objetos relacionados 
        /// </summary>
        public Curso Curso { get; set; }
        public Instructor Instructor { get; set; }
    }
}
