using Backend_Taller.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend_Taller.EFConfiguration
{
    public class ClientesConfiguration : IEntityTypeConfiguration<Clientes>
    {
        public void Configure(EntityTypeBuilder<Clientes> builder)
        {
            builder.ToTable("Clientes");

            builder.HasKey(c => c.ClientesId);

            builder.HasIndex(c => c.Nombre);

            builder.Property(c => c.RfcFisico)
                    .HasMaxLength(20)
                    .IsRequired(false);

            builder.Property(c => c.Nombre)
                    .HasMaxLength(200)
                    .IsRequired(true);

            builder.Property(c => c.Direccion)
                    .HasMaxLength(200)
                    .IsRequired(false);

            builder.Property(c => c.Cp)
                    .HasMaxLength(20)
                    .IsRequired(false);

            builder.Property(c => c.Telefono)
                    .HasMaxLength(20)
                    .IsRequired(true);

            builder.HasMany(c => c.OrdenesServicio)
                .WithOne(os => os.Cliente)
                .HasForeignKey(os => os.ClientesId);
        }
    }
}
