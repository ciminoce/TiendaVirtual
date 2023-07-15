using System.Collections.Generic;
using TiendaVirtual.Entidades.Entidades;

namespace TiendaVirtual.Servicios.Interfaces
{
    public interface IServiciosCarrito
    {
        List<ItemCarrito> GetCarrito(string user);
        void Guardar(ItemCarrito carritoTemp);
        void Borrar(string user, int productoId);
        ItemCarrito GetItem(string user, int productoId);
        int GetCantidad(string user);

    }
}
