using System.ComponentModel.DataAnnotations;

namespace Backend_Taller.DTOs
{
    public class MarcaDTO
    {
        [Required]
        [MaxLength(150)]
        public string NombreMarca { get; set; }
    }
}
