using CursoUdemyWebAPI.Modelo;
using Dapper;
using System;
using System.Buffers.Text;
using System.Data;
using System.Data.SqlClient;

namespace CursoUdemyWebAPI.Servicios
{
    public class ServicioEmpleadoSQL : IServicioEmpleadoSQL
    {
        private string CadenaConexion;
        private readonly ILogger<ServicioEmpleadoSQL> log;
        public ServicioEmpleadoSQL(ConexionBaseDatos conex, ILogger<ServicioEmpleadoSQL> l)
        {
            CadenaConexion = conex.CadenaConexionSQL;
            this.log = l;
        }

        private SqlConnection conexion() 
        {
            return new SqlConnection(CadenaConexion);
        }

        public async Task BajaEmpleado(string codEmpleado)
        {
            SqlConnection sqlConexion = conexion();
            try
            {
                sqlConexion.Open();
                var param = new DynamicParameters();
                param.Add("@CodEmpleado", codEmpleado, DbType.String, ParameterDirection.Input, 4);
                await sqlConexion.ExecuteScalarAsync("EmpleadoBorrar", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                log.LogError("ERROR: " + ex.ToString());
                throw new Exception("Se produjo un error al borrar un empleado" + ex.Message);
            }
            finally
            {
                sqlConexion.Close();
                sqlConexion.Dispose();
            }

        }

        public  async Task<Empleado> DameEmpleado(string codEmpleado)
        {
            SqlConnection sqlConexion = conexion();
            Empleado e = null;
            try
            {
                sqlConexion.Open();
                var param = new DynamicParameters();
                param.Add("@CodEmpleado", codEmpleado, DbType.String, ParameterDirection.Input, 4);
                e = await sqlConexion.QueryFirstOrDefaultAsync<Empleado>("EmpleadoObtener", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                log.LogError("ERROR: " + ex.ToString());
                throw new Exception("Se produjo un error al obtener un empleado" + ex.Message);
            }
            finally
            {
                sqlConexion.Close();
                sqlConexion.Dispose();
            }

            return e;
        }

        public async Task<IEnumerable<Empleado>> DameEmpleados()
        {
            SqlConnection sqlConexion = conexion();
            List<Empleado> empleados = new List<Empleado>();
            try
            {
                sqlConexion.Open();
                var r = await sqlConexion.QueryAsync<Empleado>("EmpleadoObtener", commandType: CommandType.StoredProcedure);
                empleados = r.ToList();
            }
            catch (Exception ex)
            {
                log.LogError("ERROR: " + ex.ToString());
                throw new Exception("Se produjo un error al obtener los empledos\"" + ex.Message);
            }
            finally
            {
                sqlConexion.Close();
                sqlConexion.Dispose();
            }

            return empleados;
        }

        public async Task ModificarEmpleado(Empleado e)
        {
            SqlConnection sqlConexion = conexion();
            try
            {
                sqlConexion.Open();
                var param = new DynamicParameters();
                param.Add("@Nombre", e.Nombre, DbType.String, ParameterDirection.Input, 500);
                param.Add("@CodEmpleado", e.CodEmpleado, DbType.String, ParameterDirection.Input, 4);
                param.Add("@Email", e.Email, DbType.String, ParameterDirection.Input, 255);
                param.Add("@Edad", e.Edad, DbType.Int32, ParameterDirection.Input);
                await sqlConexion.ExecuteScalarAsync("EmpleadoModificar", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                log.LogError("ERROR: " + ex.ToString());
                throw new Exception("Se produjo un error al modificar empleado " + ex.Message);
            }
            finally
            {
                sqlConexion.Close();
                sqlConexion.Dispose();
            }

        }

        public async Task NuevoEmpleado(Empleado e)
        {
            SqlConnection sqlConexion = conexion();
            try
            {
                sqlConexion.Open();
                var param = new DynamicParameters();
                param.Add("@Nombre", e.Nombre, DbType.String, ParameterDirection.Input, 500);
                param.Add("@CodEmpleado", e.CodEmpleado, DbType.String, ParameterDirection.Input, 4);
                param.Add("@Email", e.Email, DbType.String, ParameterDirection.Input, 255);
                param.Add("@Edad", e.Edad, DbType.Int32, ParameterDirection.Input);
                param.Add("@FechaAlta", e.FechaAlta, DbType.DateTime, ParameterDirection.Input);
                await sqlConexion.ExecuteScalarAsync("EmpleadoAlta", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                log.LogError("ERROR: " + ex.ToString());
                throw new Exception("Se produjo un error al dar de alta" + ex.Message);
            }
            finally
            {
                sqlConexion.Close();
                sqlConexion.Dispose();
            }

        }


        public async Task GuardarImagen(string base64)
        {
            SqlConnection sqlConexion = conexion();
            try
            {
                sqlConexion.Open();
                var param = new DynamicParameters();
                param.Add("@ImagenBase64", base64, DbType.String, ParameterDirection.Input,int.MaxValue);
                await sqlConexion.ExecuteScalarAsync("GuardarImagen", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                log.LogError("ERROR: " + ex.ToString());
                throw new Exception("Se produjo un error al guardar la imagen" + ex.Message);
            }
            finally
            {
                sqlConexion.Close();
                sqlConexion.Dispose();
            }
        }

        public async Task<String> ObtenerImagen(int id)
        {
            SqlConnection sqlConexion = conexion();
            string res = String.Empty;
            try
            {
                sqlConexion.Open();
                var param = new DynamicParameters();
                param.Add("@Id", id, DbType.Int32, ParameterDirection.Input);
                var r = await sqlConexion.QueryFirstAsync<string>("ObtnerImagen", param,commandType: CommandType.StoredProcedure);
                res= r.ToString();
            }
            catch (Exception ex)
            {
                log.LogError("ERROR: " + ex.ToString());
                throw new Exception("Se produjo un error al obtener imagen\"" + ex.Message);
            }
            finally
            {
                sqlConexion.Close();
                sqlConexion.Dispose();
            }

            return res;
        }
    }
}
