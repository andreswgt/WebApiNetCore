using CursoUdemyWebAPI.Modelo;

namespace CursoUdemyWebAPI.Servicios
{
    public class ServicioEmpleado : IServicioEmpleado
    {
        private readonly List<Empleado> listaEmpleados = new()
        {
            new Empleado{Id=1, Nombre="Juan", CodEmpleado="A001",Email="mail1@mail.es",Edad=45,FechaAlta =DateTime.Now , FechaBaja=null},
            new Empleado{Id=1, Nombre="Pedro", CodEmpleado="A010",Email="mail2@mail.es",Edad=35,FechaAlta =DateTime.Now, FechaBaja=null },
            new Empleado{Id=1, Nombre="Manolo", CodEmpleado="B017",Email="mail3@mail.es",Edad=25,FechaAlta =DateTime.Now,FechaBaja=null },
            new Empleado{Id=1, Nombre="Ana", CodEmpleado="A071",Email="mail4@mail.es",Edad=37,FechaAlta =DateTime.Now,FechaBaja=null },
        };

        public IEnumerable<Empleado> DameEmpleados()
        {
            return listaEmpleados;
        }

        public Empleado DameEmpleado(string codEmpleado)
        {
            return listaEmpleados.Where(e => e.CodEmpleado == codEmpleado).SingleOrDefault();
        }

        public void NuevoEmpleado(Empleado e)
        {
            listaEmpleados.Add(e);
        }

        public void ModificarEmpleado(Empleado e)
        {
            int posicion = listaEmpleados.FindIndex(existeEmpleado => existeEmpleado.Id == e.Id);
            if (posicion != -1)
                listaEmpleados[posicion] = e;
        }

        public void BajaEmpleado(string codEmpleado) 
        {
            int posicion = listaEmpleados.FindIndex(existeEmpleado => existeEmpleado.CodEmpleado == codEmpleado);
            if (posicion != -1)
                listaEmpleados.RemoveAt(posicion);
        }

        public void GuardarImagen(string base64)
        { 
            
        }

    }
}
