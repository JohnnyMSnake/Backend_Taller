namespace Backend_Taller.Models
{
    public class Vehiculos
    {
        public int VehiculosId { get; set; }
        public string NumeroSerie { get; set; }
        public string Placas { get; set; }
        public string Tipo { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string NumeroMotor { get; set; }
        public string Color { get; set; }

        public ICollection<OrdenServicio> OrdenesServicio { get; set; }
    }
}
