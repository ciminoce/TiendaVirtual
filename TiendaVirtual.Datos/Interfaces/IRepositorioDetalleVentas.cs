using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaVirtual.Entidades.Dtos.DetalleVenta;
using TiendaVirtual.Entidades.Entidades;

namespace TiendaVirtual.Datos.Interfaces
{
    public interface IRepositorioDetalleVentas
    {
        void Agregar(DetalleVenta detalle);
        void Borrar(int id);
        void Editar(DetalleVenta detalle);
        bool EstaRelacionado(DetalleVenta detalle);
        bool Existe(DetalleVenta detalle);
        DetalleVenta GetDetalleVentaPorId(int id);
        List<DetalleVentaListDto> GetDetalleVentas(int ventaId);

    }
}
