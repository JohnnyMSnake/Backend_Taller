namespace Backend_Taller.Models
{
    public class RfcMoral
    {
        public int RfcMoralId { get; set; }
        public string RfcMoralValue { get; set; }
        public string Institucion { get; set; }

        public ICollection<OrdenServicio> OrdenesServicio { get; set; }

    }
}
