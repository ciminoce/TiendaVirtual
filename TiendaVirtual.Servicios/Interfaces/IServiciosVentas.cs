using System.Collections.Generic;
using TiendaVirtual.Entidades.Dtos.DetalleVenta;
using TiendaVirtual.Entidades.Dtos.Venta;
using TiendaVirtual.Entidades.Entidades;

namespace TiendaVirtual.Servicios.Interfaces
{
    public interface IServiciosVentas
    {
        int GetCantidad();
        List<DetalleVentaListDto> GetDetalleVenta(int ventaId);
        List<VentaListDto> GetVentas();
        VentaListDto GetVentasPorId(int value);
        void Guardar(Venta venta, string name);
    }
}
