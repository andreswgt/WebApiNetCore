using CursoUdemyWebAPI.Modelo;

namespace CursoUdemyWebAPI.Servicios
{
    public interface IServicioUsuarioAPI
    {      
        Task<UsuarioAPI> DameUsuario(LoginAPI login);
    }
}
