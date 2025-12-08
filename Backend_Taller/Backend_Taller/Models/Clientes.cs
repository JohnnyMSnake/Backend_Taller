using System.ComponentModel.DataAnnotations;

namespace Backend_Taller.Models
{
    public class Clientes
    {
        public int ClientesId { get; set; }
        public string RfcFisico { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Cp { get; set; }
        [Phone]
        public string Telefono { get; set; }

        public ICollection<OrdenServicio> OrdenesServicio { get; set; }
    }
}
