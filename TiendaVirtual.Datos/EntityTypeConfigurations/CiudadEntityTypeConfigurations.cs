using System.Data.Entity.ModelConfiguration;
using TiendaVirtual.Entidades.Entidades;

namespace TiendaVirtual.Datos.EntityTypeConfigurations
{
    public class CiudadEntityTypeConfigurations:EntityTypeConfiguration<Ciudad>
    {
        public CiudadEntityTypeConfigurations()
        {
            ToTable("Ciudades");
            Property(c=>c.RowVersion).IsRowVersion().IsConcurrencyToken();
            
        }
    }
}
