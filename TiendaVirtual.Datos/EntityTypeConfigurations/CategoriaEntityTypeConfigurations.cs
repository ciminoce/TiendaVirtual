using System.Data.Entity.ModelConfiguration;
using TiendaVirtual.Entidades.Entidades;

namespace TiendaVirtual.Datos.EntityTypeConfigurations
{
    public class CategoriaEntityTypeConfigurations : EntityTypeConfiguration<Categoria>
    {
        public CategoriaEntityTypeConfigurations()
        {
            ToTable("Categorias");
            Property(c => c.RowVersion).IsRowVersion().IsConcurrencyToken();

        }
    }
}
