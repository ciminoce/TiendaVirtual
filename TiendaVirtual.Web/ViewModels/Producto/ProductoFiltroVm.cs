using PagedList;
using System.Collections.Generic;
using System.Web.Mvc;

namespace TiendaVirtual.Web.ViewModels.Producto
{
    public class ProductoFiltroVm
    {
        public List<SelectListItem> Categorias { get; set; }
        public IPagedList<ProductoListVm> Productos { get; set; }
        public int? CategoriaFiltro { get; set; }

    }
}