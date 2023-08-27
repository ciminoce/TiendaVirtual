using System.Collections.Generic;
using System.Web.Mvc;
using TiendaVirtual.Entidades.Dtos.Categoria;
using TiendaVirtual.Entidades.Entidades;

namespace TiendaVirtual.Datos.Interfaces
{
    public interface IRepositorioCategorias
    {
        List<CategoriaListDto> GetCategorias();
        void Agregar(Categoria categoria);
        void Editar(Categoria categoria);
        void Borrar(int id);
        bool Existe(Categoria categoria);
        Categoria GetCategoriaPorId(int categoriaId);
        bool EstaRelacionado(Categoria categoria);
        List<Categoria> GetCategoriasPorPagina(int cantidad, int pagina);
        int GetCantidad();
        List<SelectListItem> GetCategoriasDropDownList();
    }
}
