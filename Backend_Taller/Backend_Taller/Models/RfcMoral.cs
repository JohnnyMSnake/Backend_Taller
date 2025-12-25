using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Backend_Taller.Models
{
    [Index(nameof(Institucion))]
    public class RfcMoral
    {
        public int RfcMoralId { get; set; }
        [Required]
        [MaxLength(20)]
        public string RfcMoralValue { get; set; }
        [Required]
        [MaxLength(200)]
        public string Institucion { get; set; }

        public ICollection<OrdenServicio> OrdenesServicio { get; set; }

    }
}
