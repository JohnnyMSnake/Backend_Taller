using System.ComponentModel.DataAnnotations;

namespace Backend_Taller.DTOs
{
    public class RfcMoralDTO
    {
        public int RfcMoralId { get; set; }
        [Required]
        [MaxLength(20)]
        public string RfcMoralValue { get; set; }
        [Required]
        [MaxLength(200)]
        public string Institucion { get; set; }
    }
}
