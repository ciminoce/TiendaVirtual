using System.Collections.Generic;
using TiendaVirtual.Entidades.Dtos.DetalleVenta;

namespace TiendaVirtual.Entidades.Dtos.Venta
{
    public class VentaDetalleDto
    {
        public VentaListDto venta { get; set; }
        public List<DetalleVentaListDto> detalleVenta { get; set; }
    }
}
