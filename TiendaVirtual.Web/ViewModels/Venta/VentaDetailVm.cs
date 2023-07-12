using System.Collections.Generic;
using TiendaVirtual.Web.ViewModels.DetalleVenta;

namespace TiendaVirtual.Web.ViewModels.Venta
{
    public class VentaDetailVm
    {
        public VentaListVm Venta { get; set; }
        public List<DetalleVentaListVm> Detalles { get; set; }
    }
}