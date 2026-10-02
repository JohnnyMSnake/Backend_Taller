using System.ComponentModel.DataAnnotations;

namespace Backend_Taller.DTOs
{
    public class ClientesDTO
    {
        public int ClientesId { get; set; }
        [MaxLength(20)]
        public string? RfcFisico { get; set; }
        [Required]
        [MaxLength(200)]
        public string Nombre { get; set; }
        [MaxLength(200)]
        public string? Direccion { get; set; }
        [MaxLength(20)]
        public string? Cp { get; set; }
        [MaxLength(20)]
        [Required]
        [Phone]
        public string Telefono { get; set; }
    }
}
