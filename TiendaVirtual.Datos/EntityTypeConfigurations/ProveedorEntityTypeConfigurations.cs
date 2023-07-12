using System.Data.Entity.ModelConfiguration;
using TiendaVirtual.Entidades.Entidades;

namespace TiendaVirtual.Datos.EntityTypeConfigurations
{
    public class ProveedorEntityTypeConfigurations:EntityTypeConfiguration<Proveedor>
    {
        public ProveedorEntityTypeConfigurations()
        {
            ToTable("Proveedores");
            Property(c => c.Id).HasColumnName("ProveedorId");
            Property(c => c.Nombre).HasColumnName("RazonSocial");
            Property(c => c.RowVersion).IsRowVersion().IsConcurrencyToken();

        }
    }
}
