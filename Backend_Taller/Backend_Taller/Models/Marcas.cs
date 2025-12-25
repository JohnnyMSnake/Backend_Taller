using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Backend_Taller.Models
{
    public class Marcas
    {
        public int MarcasId { get; set; }
        [Required]
        [MaxLength(150)]
        public string NombreMarca { get; set; }
        [JsonIgnore]
        public ICollection<Vehiculos> Vehiculos { get; set; }
    }
}
