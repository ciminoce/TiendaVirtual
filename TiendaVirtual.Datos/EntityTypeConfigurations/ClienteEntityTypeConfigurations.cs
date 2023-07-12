using System.Data.Entity.ModelConfiguration;
using TiendaVirtual.Entidades.Entidades;

namespace TiendaVirtual.Datos.EntityTypeConfigurations
{
    public class ClienteEntityTypeConfigurations:EntityTypeConfiguration<Cliente>
    {
        public ClienteEntityTypeConfigurations()
        {
            ToTable("Clientes");
            Property(c => c.Id).HasColumnName("ClienteId");
            Property(c => c.Nombre).HasColumnName("NombreCliente");
            Property(c => c.TelefonoFijo).HasColumnName("TelFijo");
            Property(c => c.TelefonoMovil).HasColumnName("TelMovil");
            Property(c=>c.RowVersion).IsRowVersion().IsConcurrencyToken();
        }
    }
}
