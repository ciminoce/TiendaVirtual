using System;
using System.Collections.Generic;
using TiendaVirtual.Datos.Interfaces;
using TiendaVirtual.Entidades.Entidades;

namespace TiendaVirtual.Datos.Repositorios
{
    public class RepositorioCarritos : IRepositorioCarritos
    {
        public void Agregar(ItemCarrito carritoTemp)
        {
            throw new NotImplementedException();
        }

        public void Borrar(ItemCarrito carritoTemp)
        {
            throw new NotImplementedException();
        }

        public void Editar(ItemCarrito carritoTemp)
        {
            throw new NotImplementedException();
        }

        public int GetCantidad(string user)
        {
            throw new NotImplementedException();
        }

        public List<ItemCarrito> GetCarrito(string user)
        {
            throw new NotImplementedException();
        }

        public ItemCarrito GetItem(string user, int productoId)
        {
            throw new NotImplementedException();
        }
    }
}
