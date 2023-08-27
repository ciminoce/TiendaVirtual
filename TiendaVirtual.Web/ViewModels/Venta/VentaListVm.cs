using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TiendaVirtual.Web.ViewModels.Venta
{
    public class VentaListVm
    {
        [DisplayName("Vta. Nro.")]
        public int VentaId { get; set; }
        [DisplayName("Fec. Vta.")]
        [DataType(DataType.Date)]
        public DateTime FechaVenta { get; set; }
        public string Cliente { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }

    }
}