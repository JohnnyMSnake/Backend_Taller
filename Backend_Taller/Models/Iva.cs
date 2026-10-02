using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_Taller.Models
{
    public class Iva
    {
        public int IvaId { get; set; }
        [Required]
        [Column(TypeName = "decimal(4,2)")]
        public decimal IvaValue { get; set; }
    }
}
