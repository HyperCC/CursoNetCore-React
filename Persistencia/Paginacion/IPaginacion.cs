using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistencia.Paginacion
{
    // LA CARPETA PAGINACION DEBIA IR DENTRO DE LA CARPETA DAPPERCONEXION
    public interface IPaginacion
    {
        Task<PaginacionModel> DevolverPaginacion(string storeProcedure,
            int numeroPagina,
            int cantidadElementos,
            // parametros para el filtro de la busueda
            IDictionary<string, object> parametrosFiltro,
            string ordenamientoColumnas
            );
    }
}
