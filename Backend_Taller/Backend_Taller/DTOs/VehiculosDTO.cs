using System.ComponentModel.DataAnnotations;

namespace Backend_Taller.DTOs
{
    public class VehiculosDTO
    {
        public int VehiculosId { get; set; }
        [MaxLength(100)]
        public string? NumeroSerie { get; set; }
        [Required]
        [MaxLength(20)]
        public string Placas { get; set; }
        [MaxLength(50)]
        public string? Tipo { get; set; }
        [Required]
        public int MarcasId { get; set; }
        [MaxLength(100)]
        public string? Modelo { get; set; }
        [MaxLength(100)]
        public string? NumeroMotor { get; set; }
        [MaxLength(50)]
        public string? Color { get; set; }
    }
}
