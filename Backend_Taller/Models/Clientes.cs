using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Backend_Taller.Models
{
    [Index(nameof(Nombre))]
    public class Clientes
    {
        public int ClientesId { get; set; }
        [MaxLength(20)]
        public string? RfcFisico { get; set; }
        [MaxLength(200)]
        [Required]
        public string Nombre { get; set; }
        [MaxLength(200)]
        public string? Direccion { get; set; }
        [MaxLength(20)]
        public string? Cp { get; set; }
        [MaxLength(20)]
        [Required]
        [Phone]
        public string Telefono { get; set; }

        public ICollection<OrdenServicio> OrdenesServicio { get; set; }
    }
}
