namespace TiendaVirtual.Entidades.Entidades
{
    public class ItemCarrito
    {
        public int ItemCarritoId { get; set; }
        public int ProductoId { get; set; }
        public string UserName { get; set; }
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioTotal => Cantidad * PrecioUnitario;

    }
}
