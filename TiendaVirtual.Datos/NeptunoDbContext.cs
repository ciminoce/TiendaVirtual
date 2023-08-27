using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Reflection;
using TiendaVirtual.Entidades.Entidades;

namespace TiendaVirtual.Datos
{
    public class NeptunoDbContext : DbContext
    {
        public NeptunoDbContext()
        {

        }
        public DbSet<DetalleVenta> DetalleVentas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Ciudad> Ciudades { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<ItemCarrito> Carrito { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<NeptunoDbContext>(null);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
