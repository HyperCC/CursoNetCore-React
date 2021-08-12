using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Cursos
{
    // clase DTO para los instructores
    public class InstructorDTO
    {
        public Guid InstructorId { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Grado { get; set; }
        public byte[] FotoPerfil { get; set; }

        public DateTime? FechaCreacion { get; set; }
    }
}
