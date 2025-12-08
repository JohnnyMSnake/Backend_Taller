using Microsoft.EntityFrameworkCore;

namespace Backend_Taller.Models
{
    public class TallerDbContext : DbContext
    {
        public TallerDbContext(DbContextOptions<TallerDbContext> options) : base(options)
        {
        }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Vehiculos> Vehiculos { get; set; }
        public DbSet<RfcMoral> RfcMorales { get; set; }
        public DbSet<OrdenServicio> OrdenesServicio { get; set; }
        public DbSet<Servicios> Servicios { get; set; }
        public DbSet<Presupuestos> Presupuestos { get; set; }
        public DbSet<Iva> Iva { get; set; }
    }
}
