using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.DapperConexion.Instructor
{
    // operaciones basicas para Instructor en los pocedimientos almacenados 
    public interface IInstructor
    {
        Task<IEnumerable<InstructorModel>> ObtenerLista();
        Task<InstructorModel> ObtenerPorId(Guid id);

        // estos metodos deben retornar tipo int porue la DB retorna la lista de transacciones exitosas (un numero)
        Task<int> Nuevo(string nombre, string apellidos, string tituloS);
        Task<int> Editar(Guid instructorId, string nombre, string apellidos, string titulo);
        Task<int> Eliminar(Guid id);
    }
}
