using System;
using System.Collections.Generic;
using TiendaVirtual.Entidades.Dtos.Producto;
using TiendaVirtual.Entidades.Entidades;

namespace TiendaVirtual.Datos.Interfaces
{
    public interface IRepositorioProductos
    {
        void Agregar(Producto producto);
        void Borrar(int id);
        void Editar(Producto producto);
        bool EstaRelacionado(Producto producto);
        bool Existe(Producto producto);
        Producto GetProductoPorId(int id);
        List<ProductoListDto> GetProductos(bool todos);
        List<ProductoListDto> GetProductos(int categoriaId);
        List<ProductoListDto> Filtrar(Func<Producto, bool> predicado);
        void ActualizarStock(int productoId, int cantidad);
        int GetCantidad(Func<Producto, bool> predicado);
    }
}
