using System.Data.Entity.ModelConfiguration;
using TiendaVirtual.Entidades.Entidades;

namespace TiendaVirtual.Datos.EntityTypeConfigurations
{
    public class ProductoEntityTypeConfigurations:EntityTypeConfiguration<Producto>
    {
        public ProductoEntityTypeConfigurations()
        {
            ToTable("Productos");
            Property(p=>p.RowVersion).IsRowVersion().IsConcurrencyToken();
        }
    }
}
