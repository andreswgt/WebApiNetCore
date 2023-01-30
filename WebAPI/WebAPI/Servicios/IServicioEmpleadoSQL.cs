using CursoUdemyWebAPI.Modelo;

namespace CursoUdemyWebAPI.Servicios
{
    public interface IServicioEmpleadoSQL
    {
        public Task<IEnumerable<Empleado>> DameEmpleados();
        public Task<Empleado> DameEmpleado(string codEmpleado);
        public Task NuevoEmpleado(Empleado e);
        public Task ModificarEmpleado(Empleado e);
        public Task BajaEmpleado(string codEmpleado);
        public Task GuardarImagen(string base64);
        public Task<String> ObtenerImagen(int id);

    }
}
