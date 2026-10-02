using System.ComponentModel.DataAnnotations;

namespace Backend_Taller.Models
{
    public class Presupuestos
    {
        public int PresupuestosId { get; set; }
        public int OrdenServicioId { get; set; }
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
        public OrdenServicio OrdenServicio { get; set; }
    }
}
