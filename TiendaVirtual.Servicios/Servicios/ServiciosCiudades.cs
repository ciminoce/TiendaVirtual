using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TiendaVirtual.Datos;
using TiendaVirtual.Datos.Interfaces;
using TiendaVirtual.Entidades.Dtos.Ciudad;
using TiendaVirtual.Entidades.Entidades;
using TiendaVirtual.Servicios.Interfaces;

namespace Neptuno2022EF.Servicios.Servicios
{
    public class ServiciosCiudades : IServiciosCiudades
    {
        private readonly IRepositorioCiudades _repitorioCiudades;
        private readonly IUnitOfWork _unitOfWork;


        public ServiciosCiudades(IRepositorioCiudades repitorioCiudades, IUnitOfWork unitOfWork)
        {
            _repitorioCiudades = repitorioCiudades;
            _unitOfWork = unitOfWork;
        }

        public void Borrar(int ciudadId)
        {
            try
            {
                _repitorioCiudades.Borrar(ciudadId);
                _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool EstaRelacionada(Ciudad ciudad)
        {
            try
            {
                return _repitorioCiudades.EstaRelacionada(ciudad);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Ciudad ciudad)
        {
            try
            {
                return _repitorioCiudades.Existe(ciudad);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CiudadListDto> GetCiudades()
        {
            try
            {
                return _repitorioCiudades.GetCiudades();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Ciudad GetCiudadPorId(int ciudadId)
        {
            try
            {
                return _repitorioCiudades.GetCiudadPorId(ciudadId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CiudadListDto> GetCiudades(int paisId)
        {
            try
            {
                return _repitorioCiudades.GetCiudades(paisId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Guardar(Ciudad ciudad)
        {
            try
            {
                if (ciudad.CiudadId==0)
                {
                    _repitorioCiudades.Agregar(ciudad);
                   
                }
                else
                {
                    _repitorioCiudades.Editar(ciudad);
                }
                _unitOfWork.SaveChanges();
                

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CiudadListDto> Filtrar(Func<Ciudad, bool> predicado, int cantidad, int pagina)
        {
            try
            {
                return _repitorioCiudades.Filtrar(predicado, cantidad, pagina);
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
                return _repitorioCiudades.GetCantidad();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CiudadListDto> GetCiudadesPorPagina(int cantidad, int pagina)
        {
            try
            {
                return _repitorioCiudades.GetCiudadesPorPagina(cantidad, pagina);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCantidad(Func<Ciudad, bool> predicado)
        {
            try
            {
                return _repitorioCiudades.GetCantidad(predicado);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SelectListItem> GetCiudadesDropDownList(int paisId)
        {
            try
            {
                return _repitorioCiudades.GetCiudadesDropDownList(paisId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
