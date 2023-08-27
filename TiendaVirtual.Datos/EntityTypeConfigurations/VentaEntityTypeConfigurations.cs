using System.Data.Entity.ModelConfiguration;
using TiendaVirtual.Entidades.Entidades;

namespace TiendaVirtual.Datos.EntityTypeConfigurations
{
    public class VentaEntityTypeConfigurations:EntityTypeConfiguration<Venta>
    {
        public VentaEntityTypeConfigurations()
        {
            ToTable("Ventas");
            Property(v=>v.RowVersion).IsRowVersion().IsConcurrencyToken();
        }
    }
}
