using System.Collections.Generic;

namespace TiendaVirtual.Web.ViewModels.Carrito
{
    public class CarritoVm
    {
        public List<ItemCarritoVm> Items { get; set; }
        public string returnUrl { get; set; }
    }
}