using System;
using System.Collections.Generic;
using TiendaVirtual.Datos;
using TiendaVirtual.Datos.Repositorios;
using TiendaVirtual.Entidades.Dtos.Producto;
using TiendaVirtual.Entidades.Entidades;
using TiendaVirtual.Servicios.Interfaces;

namespace Neptuno2022EF.Servicios.Servicios
{
    public class ServiciosProductos : IServiciosProductos
    {
        private readonly RepositorioProductos _repositorio;
        private readonly IUnitOfWork _unitOfWork;


        public ServiciosProductos(RepositorioProductos repositorio, IUnitOfWork unitOfWork)
        {
            _repositorio = repositorio;
            _unitOfWork = unitOfWork;
        }

        public void ActualizarUnidadesEnPedido(int productoId, int cantidad)
        {
            try
            {
                _repositorio.ActualizarUnidadesEnPedido(productoId, cantidad);
                _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
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

        public bool EstaRelacionado(Producto producto)
        {
            try
            {
                return _repositorio.EstaRelacionado(producto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Producto producto)
        {
            try
            {
                return _repositorio.Existe(producto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ProductoListDto> Filtrar(Func<Producto, bool> predicado)
        {
            try
            {
                return _repositorio.Filtrar(predicado);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCantidad(Func<Producto, bool> predicado)
        {
            try
            {
                return _repositorio.GetCantidad(predicado);
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

        public Producto GetProductoPorId(int id)
        {
            try
            {
                return _repositorio.GetProductoPorId(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ProductoListDto> GetProductos(bool todos)
        {
            try
            {
                return _repositorio.GetProductos(todos);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<ProductoListDto> GetProductos(int categoriaId)
        {
            try
            {
                return _repositorio.GetProductos(categoriaId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Guardar(Producto producto)
        {
            try
            {
                if (producto.ProductoId==0)
                {
                    _repositorio.Agregar(producto);
                }
                else
                {
                    _repositorio.Editar(producto);
                }
                _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
