using CursoUdemyWebAPI.DTO;
using CursoUdemyWebAPI.Modelo;

namespace CursoUdemyWebAPI
{
    public static class Utilidades
    {
        public static EmpleadoDTO convertirDTO(this Empleado e)
        {
            if (e != null)
            {
                return new EmpleadoDTO
                {
                    Nombre = e.Nombre,
                    CodEmpleado = e.CodEmpleado,
                    Email = e.Email,
                    Edad = e.Edad
                };
            }

            return null;
        }

        public static UsuarioApiDTO convertirDTO(this UsuarioAPI u)
        {
            if (u != null)
            {
                return new UsuarioApiDTO
                {
                    Token = u.Token,
                    Usuario = u.Usuario

                };
            }

            return null;
        }
    }


}
