using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TiendaVirtual.Web.ViewModels.Ciudad;

namespace TiendaVirtual.Web.ViewModels.Pais
{
    public class PaisDetailVm
    {
        public PaisListVm Pais { get; set; }
        public List<CiudadListVm> Ciudades { get; set; }
    }
}