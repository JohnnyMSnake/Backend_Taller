namespace Backend_Taller.Models
{
    public class OrdenServicio
    {
        public int OrdenServicioId { get; set; }
        public int ClientesId { get; set; }
        public int VehiculosId { get; set; }
        public int RfcMoralId { get; set; }
        public int Kilometraje { get; set; }
        public string Observaciones { get; set; }
        public DateTime FechaEntrada { get; set; }

        public Clientes Cliente { get; set; }
        public Vehiculos Vehiculo { get; set; }
        public RfcMoral RfcMoral { get; set; }

        public ICollection<Servicios> Servicios { get; set; }

        public Presupuestos Presupuesto { get; set; }
    }
}
