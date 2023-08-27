using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TiendaVirtual.Entidades.Entidades;

namespace TiendaVirtual.Datos.Interfaces
{
    public interface IRepositorioPaises
    {
        List<Pais> GetPaises();
        void Agregar(Pais pais);
        void Editar(Pais pais);
        void Borrar(int id);
        bool Existe(Pais pais);
        Pais GetPaisPorId(int paisId);
        bool EstaRelacionado(Pais pais);
        List<Pais> GetPaisesPorPagina(int cantidad, int pagina);
        int GetCantidad();
        List<SelectListItem> GetPaisesDropDownList();
            
    }
}
