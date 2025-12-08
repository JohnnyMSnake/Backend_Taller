namespace Backend_Taller.Models
{
    public class Presupuestos
    {
        public int PresupuestosId { get; set; }
        public int OrdenServicioId { get; set; }
        public decimal ManoObra { get; set; }
        public decimal Refacciones { get; set; }
        public decimal OtrosMateriales { get; set; }
        public decimal CargosAdicionales { get; set; }
        public decimal Seguro { get; set; }
        public decimal IVA { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public decimal Anticipo { get; set; }
        public decimal Resta { get; set; }
        public OrdenServicio OrdenServicio { get; set; }
    }
}
