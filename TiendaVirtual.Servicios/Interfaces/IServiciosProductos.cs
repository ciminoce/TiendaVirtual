using System;
using System.Collections.Generic;
using TiendaVirtual.Entidades.Dtos.Producto;
using TiendaVirtual.Entidades.Entidades;

namespace TiendaVirtual.Servicios.Interfaces
{
    public interface IServiciosProductos
    {
        void Guardar(Producto producto);
        void Borrar(int id);
        bool EstaRelacionado(Producto producto);
        bool Existe(Producto producto);
        Producto GetProductoPorId(int id);
        List<ProductoListDto> GetProductos(bool todos);
        List<ProductoListDto> GetProductos(int categoriaId);
        List<ProductoListDto> Filtrar(Func<Producto, bool> predicado);
        void ActualizarUnidadesEnPedido(int productoId, int cantidad);
        int GetCantidad(Func<Producto, bool> value);
        int GetCantidad();
    }
}
