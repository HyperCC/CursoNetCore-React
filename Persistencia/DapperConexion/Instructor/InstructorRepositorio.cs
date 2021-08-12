using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Persistencia.DapperConexion.Instructor
{
    // implementacion de las uery para implementar las operaciones basicas.
    public class InstructorRepositorio : IInstructor
    {
        // conexion a la DB para los procedimientos almacenados 
        private readonly IFactoryConnection _factoryConnection;
        public InstructorRepositorio(IFactoryConnection factoryConnection)
        {
            this._factoryConnection = factoryConnection;
        }

        public async Task<int> Editar(Guid instructorId, string nombre, string apellidos, string titulo)
        {
            var storeProcedure = "usp_instructor_editar";
            try
            {
                var conexion = this._factoryConnection.GetConnection();
                var resultado = await conexion.ExecuteAsync(
                    storeProcedure, new
                    {
                        InstructorId = instructorId,
                        Nombre = nombre,
                        Apellidos = apellidos,
                        Titulo = titulo
                    },
                    commandType: CommandType.StoredProcedure
                    );

                this._factoryConnection.CloseConnection();
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo editar el Instructor especificado - procedimiento almacenado EDITAR - ", ex);
            }
        }

        public async Task<int> Eliminar(Guid id)
        {
            var storeProcedure = "usp_instructor_elimina";
            try
            {
                var connection = this._factoryConnection.GetConnection();
                var resultado = await connection.ExecuteAsync(
                    storeProcedure, new
                    {
                        InstructorId = id
                    },
                    commandType: CommandType.StoredProcedure);

                this._factoryConnection.CloseConnection();
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo eliminar el Instructor por ID - procedimiento almacenado ELIMINAR - ", ex);
            }
        }

        public async Task<int> Nuevo(string nombre, string apellidos, string titulo)
        {
            var storeProcedure = "usp_instructor_nuevo";
            try
            {
                var connection = this._factoryConnection.GetConnection();
                var result = await connection.ExecuteAsync(storeProcedure, new
                {
                    // enumeracion de los datos para el procedimiento almacenado
                    InstructorId = Guid.NewGuid(),
                    Nombre = nombre,
                    Apellidos = apellidos,
                    Titulo = titulo
                }
                , commandType: CommandType.StoredProcedure);

                this._factoryConnection.CloseConnection();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("no se pudo guardar el nuevo instructor - procedimiento almacenado NUEVO - ", ex);
            }
        }

        public async Task<IEnumerable<InstructorModel>> ObtenerLista()
        {
            IEnumerable<InstructorModel> instructorList = null;
            //nombre del procedimiento almacenado en slServer
            var storeProcedure = "usp_obtener_instructores";

            try
            {
                var connection = this._factoryConnection.GetConnection();
                // retorna una lista de data de instructores.
                instructorList = await connection.QueryAsync<InstructorModel>(storeProcedure, null, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en obtencion de datos - procedimiento almacenado OBTENER LISTA - ", ex);
            }
            finally
            {
                this._factoryConnection.CloseConnection();
            }

            return instructorList;
        }

        public async Task<InstructorModel> ObtenerPorId(Guid id)
        {
            var storeProcedure = "usp_obtener_instructor_por_id";
            InstructorModel instructor = null;

            try
            {
                var connection = this._factoryConnection.GetConnection();
                // buscar el instructor con ID coincidente
                instructor = await connection.QueryFirstAsync<InstructorModel>(
                    storeProcedure, new
                    {
                        Id = id
                    },
                    commandType: CommandType.StoredProcedure);

                this._factoryConnection.CloseConnection();
                return instructor;
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo encontrar el Instructor - procedimiento almacenado OBTENER POR ID - ", ex);
            }
        }
    }
}
