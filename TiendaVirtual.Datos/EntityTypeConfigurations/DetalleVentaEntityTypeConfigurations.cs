using System.Data.Entity.ModelConfiguration;
using TiendaVirtual.Entidades.Entidades;

namespace TiendaVirtual.Datos.EntityTypeConfigurations
{
    public class DetalleVentaEntityTypeConfigurations : EntityTypeConfiguration<DetalleVenta>
    {
        public DetalleVentaEntityTypeConfigurations()
        {
            ToTable("DetalleVentas");
            Property(d=>d.RowVersion).IsRowVersion().IsConcurrencyToken();
        }
    }
}
