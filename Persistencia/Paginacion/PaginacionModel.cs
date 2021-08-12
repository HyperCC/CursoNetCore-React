using System;
using System.Collections.Generic;
using System.Text;

namespace Persistencia.Paginacion
{
    // LA CARPETA PAGINACION DEBIA IR DENTRO DE LA CARPETA DAPPERCONEXION
    public class PaginacionModel
    {
        //IDctionary es una lista Diccionario similar a los diccionarios de python pero en json
        // lista con los resultados coincidentes
        public List<IDictionary<string, object>> ListaRecords { get; set; }

        public int TotalRecords { get; set; }
        public int NumeroPaginas { get; set; }
    }
}
