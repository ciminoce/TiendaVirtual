using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TiendaVirtual.Web.ViewModels.Cliente
{
    public class ClienteListVm
    {
        public int ClienteId { get; set; }
        [DisplayName("Cliente")]
        public string NombreCliente { get; set; }
        public string Pais { get; set; }
        public string Ciudad { get; set; }

    }
}