using System.Data.Entity.ModelConfiguration;
using TiendaVirtual.Entidades.Entidades;

namespace TiendaVirtual.Datos.EntityTypeConfigurations
{
    public class PaisEntityTypeConfigurations:EntityTypeConfiguration<Pais>
    {
        public PaisEntityTypeConfigurations()
        {
            ToTable("Paises");
            Property(p=>p.RowVersion).IsRowVersion().IsConcurrencyToken();
        }
    }
}
