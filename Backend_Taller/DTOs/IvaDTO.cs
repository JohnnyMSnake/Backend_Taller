using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_Taller.DTOs
{
    public class IvaDTO
    {
        [Required]
        [Column(TypeName = "decimal(4,2)")]
        public decimal IvaValue { get; set; }
    }
}
