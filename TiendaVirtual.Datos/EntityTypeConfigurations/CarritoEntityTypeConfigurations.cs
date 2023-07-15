using System.Data.Entity.ModelConfiguration;
using TiendaVirtual.Entidades.Entidades;

namespace TiendaVirtual.Datos.EntityTypeConfigurations
{
    public class CarritoEntityTypeConfigurations : EntityTypeConfiguration<ItemCarrito>
    {
        public CarritoEntityTypeConfigurations()
        {
            ToTable("Carritos");
        }
    }
}
