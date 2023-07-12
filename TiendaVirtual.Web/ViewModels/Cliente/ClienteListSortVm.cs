using PagedList;
using System.Collections.Generic;

namespace TiendaVirtual.Web.ViewModels.Cliente
{
    public class ClienteListSortVm
    {
        public IPagedList<ClienteListVm> Clientes { get; set; }
        public string SortBy { get; set; }
        public Dictionary<string, string> Sorts { get; set; }
        public string SearchBy { get; set; }
    }
}