using System.ComponentModel.DataAnnotations;

namespace Backend_Taller.DTOs
{
    public class ServiciosDTO
    {
        [Required]
        [MaxLength(150)]
        public string Descripcion { get; set; }
        public int? Clave { get; set; }
        public int? Numero { get; set; }
    }
}
