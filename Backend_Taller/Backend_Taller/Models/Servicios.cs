namespace Backend_Taller.Models
{
    public class Servicios
    {
        public int ServiciosId { get; set; }
        public int OrdenServicioId { get; set; }
        public string Descripcion { get; set; }
        public int Clave { get; set; }
        public int Numero { get; set; }


        public OrdenServicio OrdenServicio { get; set; }

    }
}
