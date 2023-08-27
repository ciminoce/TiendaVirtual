using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TiendaVirtual.Entidades.Dtos.Ciudad;
using TiendaVirtual.Entidades.Entidades;

namespace TiendaVirtual.Datos.Interfaces
{
    public interface IRepositorioCiudades
    {
        List<CiudadListDto> GetCiudades();
        void Agregar(Ciudad ciudad);
        bool Existe(Ciudad ciudad);
        void Editar(Ciudad ciudad);
        void Borrar(int ciudadId);
        bool EstaRelacionada(Ciudad ciudad);
        Ciudad GetCiudadPorId(int ciudadId);
        List<CiudadListDto> GetCiudades(int paisId);
        List<CiudadListDto> Filtrar(Func<Ciudad, bool> predicado, int cantidad, int pagina);
        int GetCantidad();
        List<CiudadListDto> GetCiudadesPorPagina(int cantidad, int pagina);
        int GetCantidad(Func<Ciudad, bool> predicado);
        List<SelectListItem> GetCiudadesDropDownList(int paisId);
    }
}
