using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TiendaVirtual.Datos;
using TiendaVirtual.Datos.Interfaces;
using TiendaVirtual.Entidades.Dtos.Categoria;
using TiendaVirtual.Entidades.Entidades;
using TiendaVirtual.Servicios.Interfaces;

namespace Neptuno2022EF.Servicios.Servicios
{
    public class ServiciosCategorias:IServiciosCategorias
    {
        private readonly IRepositorioCategorias _repositorio;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly NeptunoDbContext _context;

        public ServiciosCategorias(IRepositorioCategorias repositorio, IUnitOfWork unitOfWork)
        {
            _repositorio = repositorio;
            _unitOfWork = unitOfWork;
        }

        public void Borrar(int id)
        {
            try
            {
                _repositorio.Borrar(id);
                _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool EstaRelacionado(Categoria categoria)
        {
            try
            {
                return _repositorio.EstaRelacionado(categoria);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Categoria categoria)
        {
            try
            {
                return _repositorio.Existe(categoria);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCantidad()
        {
            try
            {
                return _repositorio.GetCantidad();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CategoriaListDto> GetCategorias()
        {
            try
            {
                return _repositorio.GetCategorias();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Categoria GetCategoriaPorId(int categoriaId)
        {
            try
            {
                return _repositorio.GetCategoriaPorId(categoriaId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Categoria> GetCategoriaPorPagina(int cantidad, int pagina)
        {
            try
            {
                return _repositorio.GetCategoriasPorPagina(cantidad, pagina);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Guardar(Categoria categoria)
        {
            try
            {
                if (categoria.CategoriaId == 0)
                {
                    _repositorio.Agregar(categoria);

                }
                else
                {
                    _repositorio.Editar(categoria);
                }
                _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<Categoria> GetCategoriasPorPagina(int cantidad, int pagina)
        {
            try
            {
                return _repositorio.GetCategoriasPorPagina(cantidad, pagina);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SelectListItem> GetCategoriasDropDownList()
        {
            try
            {
                return _repositorio.GetCategoriasDropDownList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
