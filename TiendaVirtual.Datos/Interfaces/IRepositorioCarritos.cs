using System.Collections.Generic;
using TiendaVirtual.Entidades.Entidades;

namespace TiendaVirtual.Datos.Interfaces
{
    public interface IRepositorioCarritos
    {
        List<ItemCarrito> GetCarrito(string user);
        void Guardar(ItemCarrito carritoTemp);
        ItemCarrito GetItem(string user, int productoId);
        int GetCantidad(string user);
        void Borrar(string user, int productoId);
    }
}
