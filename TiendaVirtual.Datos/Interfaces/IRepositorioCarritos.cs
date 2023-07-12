using System.Collections.Generic;
using TiendaVirtual.Entidades.Entidades;

namespace TiendaVirtual.Datos.Interfaces
{
    public interface IRepositorioCarritos
    {
        List<ItemCarrito> GetCarrito(string user);
        void Agregar(ItemCarrito carritoTemp);
        void Editar(ItemCarrito carritoTemp);
        void Borrar(ItemCarrito carritoTemp);
        ItemCarrito GetItem(string user, int productoId);
        int GetCantidad(string user);
    }
}
