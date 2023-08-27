using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TiendaVirtual.Web.ViewModels.Ciudad
{
    public class CiudadListVm
    {
        public int CiudadId { get; set; }
        [DisplayName("Ciudad")]
        public string NombreCiudad { get; set; }
        [DisplayName("País")]
        public string NombrePais { get; set; }

    }
}