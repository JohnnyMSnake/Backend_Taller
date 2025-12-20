using System.ComponentModel.DataAnnotations;

namespace Backend_Taller.DTOs
{
    public class CrearOrdenServicioDTO
    {
        [Required]
        public ClientesDTO Cliente { get; set; }
        [Required]
        public VehiculosDTO Vehiculo { get; set; }
        [Required]
        public PresupuestosDTO Presupuesto { get; set; }
        public RfcMoralDTO? RfcMoral { get; set; }
        public List<ServiciosDTO>? Servicios { get; set; }
        [Required]
        public int Kilometraje { get; set; }
        public string? Observaciones { get; set; }
        
    }
}
