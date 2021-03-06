using System;
using System.Collections.Generic;
using System.Text;

namespace Persistencia.DapperConexion.Instructor
{
    // modelo para las transacciones por procedimientos almacenados 
    public class InstructorModel
    {
        public Guid InstructorId { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Titulo { get; set; }

        public DateTime? FechaCreacion { get; set; }
    }
}
