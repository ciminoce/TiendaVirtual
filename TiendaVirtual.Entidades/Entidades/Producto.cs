namespace TiendaVirtual.Entidades.Entidades
{
    public class Producto
    {
        public int ProductoId { get; set; }
        public string NombreProducto { get; set; }
        public int ProveedorId { get; set; }
        public int CategoriaId { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Stock { get; set; }
        public int StockMinimo { get; set; }
        public bool Suspendido { get; set; }
        public string Imagen { get; set; }
        public int UnidadesEnPedido { get; set; }
        public byte[] RowVersion { get; set; }
        public Proveedor Proveedor { get; set; }
        public Categoria Categoria { get; set; }


        public int UnidadesDisponibles() => Stock - UnidadesEnPedido;
    }
}
