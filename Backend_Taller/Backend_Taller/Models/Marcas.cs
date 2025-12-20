using System.ComponentModel.DataAnnotations;

namespace Backend_Taller.Models
{
    public class Marcas
    {
        public int MarcasId { get; set; }
        [Required]
        [MaxLength(150)]
        public string NombreMarca { get; set; }
        public ICollection<Vehiculos> Vehiculos { get; set; }
    }
}
