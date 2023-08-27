using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using TiendaVirtual.Datos;
using TiendaVirtual.Datos.Interfaces;
using TiendaVirtual.Entidades.Dtos.Ciudad;
using TiendaVirtual.Entidades.Entidades;

namespace Neptuno2022EF.Datos.Repositorios
{
    public class RepositorioCiudades : IRepositorioCiudades
    {
        private readonly NeptunoDbContext _context;

        public RepositorioCiudades(NeptunoDbContext context)
        {
            _context = context;
        }

        public void Agregar(Ciudad ciudad)
        {
            try
            {
                //if (ciudad.Pais!=null)
                //{
                //    ciudad.Pais = null;
                    
                //}
                _context.Ciudades.Add(ciudad);
                //_context.Entry(ciudad).State = EntityState.Added;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Borrar(int ciudadId)
        {
            try
            {
                var ciudadInDb = GetCiudadPorId(ciudadId);
                if (ciudadInDb==null)
                {
                    throw new Exception("Registro dado de baja por otro usuario");
                }
                else
                {
                    //ciudadInDb.Pais = null;
                    _context.Entry(ciudadInDb).State = EntityState.Deleted;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Editar(Ciudad ciudad)
        {
            try
            {
                //if (ciudad.Pais != null)
                //{
                //    ciudad.Pais = null;

                //}
                var ciudadInDb = GetCiudadPorId(ciudad.CiudadId);

                if (ciudadInDb == null)
                {
                    throw new Exception("Registro dado de baja por otro usuario");
                }
                ciudadInDb.Pais = null;
                ciudadInDb.NombreCiudad = ciudad.NombreCiudad;
                ciudadInDb.PaisId = ciudad.PaisId;
                ciudadInDb.RowVersion = ciudad.RowVersion;

                _context.Entry(ciudadInDb).State = EntityState.Modified;
                //_context.Entry(ciudad).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public bool EstaRelacionada(Ciudad ciudad)
        {
            try
            {
                return _context.Clientes.Any(c=>c.CiudadId==ciudad.CiudadId) 
                    || _context.Proveedores.Any(p=>p.CiudadId == ciudad.CiudadId);
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
                if(ciudad.CiudadId== 0)
                {
                    return _context.Ciudades.Any(c => c.NombreCiudad == ciudad.NombreCiudad
                        && c.PaisId == ciudad.PaisId);

                }
                return _context.Ciudades.Any(c => c.NombreCiudad == ciudad.NombreCiudad
                            && c.PaisId == ciudad.PaisId 
                            && c.CiudadId!=ciudad.CiudadId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CiudadListDto> Filtrar(Func<Ciudad, bool> predicado, int cantidad, int pagina)
        {
            return _context.Ciudades.Include(c => c.Pais)
                .Where(predicado)
                .OrderBy(c=>c.NombreCiudad)
                .Skip(cantidad*(pagina-1))
                .Take(cantidad)
                .Select(c => new CiudadListDto
                {
                    CiudadId = c.CiudadId,
                    NombreCiudad = c.NombreCiudad,
                    NombrePais = c.Pais.NombrePais

                }).ToList();
        }

        public int GetCantidad()
        {
            return _context.Ciudades.Count();
        }

        public int GetCantidad(Func<Ciudad, bool> predicado)
        {
            return _context.Ciudades.Count(predicado);
        }

        public List<CiudadListDto> GetCiudades()
        {
            return _context.Ciudades.Include(c=>c.Pais)
                .Select(c=>new CiudadListDto
                {
                    CiudadId=c.CiudadId,
                    NombreCiudad=c.NombreCiudad,
                    NombrePais=c.Pais.NombrePais
                }).AsNoTracking()
                .ToList();
        }

        public List<CiudadListDto> GetCiudades(int paisId)
        {
            try
            {
                return _context.Ciudades.Include(c=>c.Pais)
                    .Where(c=>c.PaisId==paisId)
                    .Select(c=>new CiudadListDto
                    {
                        CiudadId= c.CiudadId,
                        NombreCiudad=c.NombreCiudad,
                        NombrePais=c.Pais.NombrePais
                    })
                    .AsNoTracking()
                    .ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SelectListItem> GetCiudadesDropDownList(int paisId)
        {
            var lista = GetCiudades(paisId);
            var dropDownCiudades = lista.Select(c => new SelectListItem
            {
                Text = c.NombreCiudad,
                Value = c.CiudadId.ToString()
            }).ToList();
            return dropDownCiudades;
        }

        public List<CiudadListDto> GetCiudadesPorPagina(int cantidad, int pagina)
        {
            return _context.Ciudades.Include(c => c.Pais)
                .OrderBy(c=>c.PaisId)
                .Skip(cantidad*(pagina-1))
                .Take(cantidad)
                .Select(c => new CiudadListDto
                {
                    CiudadId = c.CiudadId,
                    NombreCiudad = c.NombreCiudad,
                    NombrePais = c.Pais.NombrePais
                }).ToList();
        }

        public Ciudad GetCiudadPorId(int ciudadId)
        {
            try
            {
                return _context.Ciudades.Include(c=>c.Pais)
                    .AsNoTracking()
                    .SingleOrDefault(c=>c.CiudadId==ciudadId);  
                
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
