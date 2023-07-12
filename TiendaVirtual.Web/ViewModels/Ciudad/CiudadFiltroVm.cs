using PagedList;
using System.Collections.Generic;
using System.Web.Mvc;

namespace TiendaVirtual.Web.ViewModels.Ciudad
{
    public class CiudadFiltroVm
    {
        public List<SelectListItem> Paises { get; set; }
        public IPagedList<CiudadListVm> Ciudades { get; set; }
        public int? PaisFiltro { get; set; }
    }
}