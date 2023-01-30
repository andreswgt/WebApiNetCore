using CursoUdemyWebAPI.Modelo;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace CursoUdemyWebAPI.Servicios
{
    public class ServicioUsuarioAPI: IServicioUsuarioAPI
    {
        private string CadenaConexion;
        private readonly ILogger<ServicioUsuarioAPI> log;
        public ServicioUsuarioAPI(ConexionBaseDatos conex, ILogger<ServicioUsuarioAPI> l)
        {
            CadenaConexion = conex.CadenaConexionSQL;
            this.log = l;
        }

        private SqlConnection conexion()
        {
            return new SqlConnection(CadenaConexion);
        }

        public async Task<UsuarioAPI> DameUsuario(LoginAPI login) 
        {
            SqlConnection sqlConexion = conexion();
            UsuarioAPI u = null;
            try
            {
                sqlConexion.Open();
                var param = new DynamicParameters();
                param.Add("@UsuarioAPI", login.usuarioAPI, DbType.String, ParameterDirection.Input, 500);
                param.Add("@PassApi", login.passAPI, DbType.String, ParameterDirection.Input, 500);
                u = await sqlConexion.QueryFirstOrDefaultAsync<UsuarioAPI>("UsuarioAPIObtener", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                log.LogError("ERROR: " + ex.ToString());
                throw new Exception("Se produjo un error al obtener datos del usuario de login" + ex.Message);
            }
            finally
            {
                sqlConexion.Close();
                sqlConexion.Dispose();
            }

            return u;
        }
    }
}
