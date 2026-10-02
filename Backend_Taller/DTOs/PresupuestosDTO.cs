using System.ComponentModel.DataAnnotations;

namespace Backend_Taller.DTOs
{
    public class PresupuestosDTO
    {
        [Range(0, double.MaxValue)]
        public decimal ManoObra { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Refacciones { get; set; }
        [Range(0, double.MaxValue)]
        public decimal OtrosMateriales { get; set; }
        [Range(0, double.MaxValue)]
        public decimal CargosAdicionales { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Seguro { get; set; }
        [Range(0, double.MaxValue)]
        public decimal IVA { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Subtotal { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Total { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Anticipo { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Resta { get; set; }
    }
}
