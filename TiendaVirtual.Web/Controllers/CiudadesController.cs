using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using TiendaVirtual.Entidades.Dtos.Ciudad;
using TiendaVirtual.Entidades.Entidades;
using TiendaVirtual.Servicios.Interfaces;
using TiendaVirtual.Web.App_Start;
using TiendaVirtual.Web.ViewModels.Ciudad;

namespace TiendaVirtual.Web.Controllers
{
    public class CiudadesController : Controller
    {
        // GET: Ciudades
        private readonly IServiciosCiudades _servicio;
        private readonly IServiciosPaises _serviciosPaises;
        private readonly IMapper _mapper;
        public CiudadesController(IServiciosCiudades servicio, IServiciosPaises serviciosPaises)
        {
            _servicio = servicio;
            _mapper = AutoMapperConfig.Mapper;
            _serviciosPaises = serviciosPaises;

        }
        public ActionResult Index(int? PaisFiltro, int? page, int? pageSize)
        {
            List<CiudadListDto> lista;
            if (PaisFiltro==null)
            {
                lista = _servicio.GetCiudades();

            }
            else
            {
                lista = _servicio.GetCiudades(PaisFiltro.Value);
            }

            page = page ?? 1;
            pageSize = pageSize ?? 8;

            var listaVm = _mapper.Map<List<CiudadListVm>>(lista);
            var ciudadVm = new CiudadFiltroVm
            {
                PaisFiltro=PaisFiltro,
                Paises = _serviciosPaises.GetPaisesDropDownList(),
                Ciudades = listaVm.ToPagedList(page.Value, pageSize.Value)
            };
            return View(ciudadVm);
        }

        public ActionResult Create()
        {

            var ciudadVm = new CiudadEditVm()
            {
                Paises = _serviciosPaises.GetPaisesDropDownList(),
            };
            return View(ciudadVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CiudadEditVm ciudadVm)
        {
            if (!ModelState.IsValid)
            {

                ciudadVm.Paises = _serviciosPaises.GetPaisesDropDownList();
                return View(ciudadVm);
            }
            var ciudad = _mapper.Map<Ciudad>(ciudadVm);
            if (_servicio.Existe(ciudad))
            {

                ciudadVm.Paises = _serviciosPaises.GetPaisesDropDownList();

                ModelState.AddModelError(string.Empty, "Ciudad existente!!!");
                return View(ciudadVm);
            }
            _servicio.Guardar(ciudad);
            TempData["Msg"] = "Registro guardado satisfactoriamente";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ciudad = _servicio.GetCiudadPorId(id.Value);
            if (ciudad == null)
            {
                return HttpNotFound("Código de ciudad inexistente!!!");
            }
            var ciudadVm = _mapper.Map<CiudadListVm>(ciudad);
            return View(ciudadVm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            var ciudad = _servicio.GetCiudadPorId(id);
            var ciudadVm = _mapper.Map<CiudadListVm>(ciudad);
            try
            {
                if (!_servicio.EstaRelacionada(ciudad))
                {
                    _servicio.Borrar(ciudad.CiudadId);
                    TempData["Msg"] = "Registro borrado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Ciudad relacionada... Baja denegada!!");
                    return View(ciudadVm);
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Error al intentar borrar un registro de ciudades");
                return View(ciudadVm);

            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ciudad = _servicio.GetCiudadPorId(id.Value);
            if (ciudad == null)
            {
                return HttpNotFound("Código de ciudad inexistente!!!");
            }
            var ciudadVm = _mapper.Map<CiudadEditVm>(ciudad);


            ciudadVm.Paises = _serviciosPaises.GetPaisesDropDownList();
            return View(ciudadVm);

        }
        [HttpPost]
        public ActionResult Edit(CiudadEditVm ciudadVm)
        {
            if (!ModelState.IsValid)
            {
                ciudadVm.Paises = _serviciosPaises.GetPaisesDropDownList();
                return View(ciudadVm);
            }
            try
            {
                var ciudad = _mapper.Map<Ciudad>(ciudadVm);
                if (!_servicio.Existe(ciudad))
                {
                    _servicio.Guardar(ciudad);
                    TempData["Msg"] = "Registro editado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    ciudadVm.Paises = _serviciosPaises.GetPaisesDropDownList();

                    ModelState.AddModelError(string.Empty, "Ciudad existente!!!");
                    return View(ciudadVm);
                }
            }
            catch (Exception)
            {
                ciudadVm.Paises = _serviciosPaises.GetPaisesDropDownList();

                ModelState.AddModelError(string.Empty, "Ciudad existente!!!");
                return View(ciudadVm);
            }
        }
    }
}