namespace TiendaVirtual.Entidades.Dtos.DetalleVenta
{
    public class DetalleVentaListDto
    {
        public int DetalleVentaId { get; set; }
        public string Producto { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal => Cantidad * PrecioUnitario;
    }
}
