using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dominio
{
    public class Precio
    {
        public Guid PrecioId { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal PrecioActual { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Promocion { get; set; }

        /// <summary>
        /// llave foranea para relacionar precio y curso
        /// </summary>
        public Guid CursoId { get; set; }

        /// <summary>
        ///  metodo para obtener el objeto de tipo curso por la relacion
        /// </summary>
        public Curso Curso { get; set; }

    }
}
