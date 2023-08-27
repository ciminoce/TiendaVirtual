using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TiendaVirtual.Entidades.Entidades;
using TiendaVirtual.Servicios.Interfaces;
using TiendaVirtual.Web.App_Start;
using TiendaVirtual.Web.ViewModels.Ciudad;
using TiendaVirtual.Web.ViewModels.Pais;

namespace TiendaVirtual.Web.Controllers
{
    
    public class PaisesController : Controller
    {
        // GET: Paises
        private readonly IServiciosPaises _servicios;
        private readonly IServiciosCiudades _serviciosCiudades;
        private readonly IMapper _mapper;
        public PaisesController(IServiciosPaises servicios, IServiciosCiudades serviciosCiudades)
        {
            _servicios = servicios;
            _serviciosCiudades = serviciosCiudades;
            _mapper = AutoMapperConfig.Mapper;
        }
        public ActionResult Index(int? page, int? pageSize)
        {
            var lista=_servicios.GetPaises();
            //var lista = new List<Pais>();
            //var listaVm=GetListaPaisesLstVm(lista);
            var listaVm=_mapper.Map<List<PaisListVm>>(lista);
            listaVm.ForEach(p => p.CantidadCiudades = _serviciosCiudades
                    .GetCantidad(c => c.PaisId == p.PaisId));
            page = page ?? 1;
            pageSize = pageSize ?? 10;
            ViewBag.PageSize=pageSize;
            return View(listaVm.ToPagedList(page.Value, pageSize.Value));
        }
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PaisEditVm paisVm)
        {
            if (ModelState.IsValid)
            {
                var pais = _mapper.Map<Pais>(paisVm);
                if (_servicios.Existe(pais))
                {
                    ModelState.AddModelError(string.Empty, "País existente!!!");
                    return View(paisVm);
                }
                _servicios.Guardar(pais);
                TempData["Msg"] = "Registro guardado satisfactoriamente";
                return RedirectToAction("Index");

            }
            else
            {
                return View(paisVm);
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var pais = _servicios.GetPaisPorId(id.Value);
            if (pais == null)
            {
                return HttpNotFound("Código de país inesistente!!!");
            }
            var paisVm=_mapper.Map<PaisListVm> (pais);
            return View(paisVm);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfim(int id) {
            var pais = _servicios.GetPaisPorId(id);
            if (_servicios.EstaRelacionado(pais))
            {
                var paisVm = _mapper.Map<PaisListVm>(pais);
                ModelState.AddModelError(string.Empty, "País relacionado... Baja denegada!!!");
                return View(paisVm);

            }
            _servicios.Borrar(id);
            TempData["Msg"] = "Registro borrado satisfactoriamente!!!";
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var pais = _servicios.GetPaisPorId(id.Value);
            if (pais == null)
            {
                return HttpNotFound("Código de país inesistente!!!");
            }
            var paisVm = _mapper.Map<PaisEditVm>(pais);
            return View(paisVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PaisEditVm paisVm)
        {
            if (!ModelState.IsValid)
            {
                return View(paisVm);
            }
            var pais = _mapper.Map<Pais>(paisVm);
            if (_servicios.Existe(pais))
            {
                ModelState.AddModelError(string.Empty, "País existente!!!");
                return View(paisVm);
            }
            _servicios.Guardar(pais);
            TempData["Msg"] = "Registro editado satisfactoriamente!!!";
            return RedirectToAction("Index");

        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var pais = _servicios.GetPaisPorId(id.Value);
            if (pais == null)
            {
                return HttpNotFound("Código de país inesistente!!!");
            }
            var paisVm=_mapper.Map<PaisListVm>(pais);
            paisVm.CantidadCiudades = _serviciosCiudades
                .GetCantidad(c => c.PaisId == paisVm.PaisId);
            var paisDetailVm = new PaisDetailVm()
            {
                Pais = paisVm,
                Ciudades = _mapper.Map<List<CiudadListVm>>(
                _serviciosCiudades.GetCiudades(pais.PaisId))
            };
            return View(paisDetailVm);
        }

        public ActionResult AddCity(int? paisId)
        {
            if (paisId == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var pais = _servicios.GetPaisPorId(paisId.Value);
            if (pais == null)
            {
                return HttpNotFound("Código de país inesistente!!!");
            }
            var ciudadVm = new CiudadEditVm()
            {
                Paises = _servicios.GetPaisesDropDownList()
            };
            return View(ciudadVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCity(CiudadEditVm ciudadVm)
        {
            if (!ModelState.IsValid)
            {
                ciudadVm.Paises = _servicios.GetPaisesDropDownList();
                return View(ciudadVm);
            }
            try
            {
                var ciudad = _mapper.Map<Ciudad>(ciudadVm);
                if (!_serviciosCiudades.Existe(ciudad))
                {
                    _serviciosCiudades.Guardar(ciudad);
                    return RedirectToAction($"Details/{ciudad.PaisId}");
                }
                else
                {
                    ciudadVm.Paises = _servicios.GetPaisesDropDownList();
                    ModelState.AddModelError(string.Empty, "Ciudad existente!!!");
                    return View(ciudadVm);
                }
            }
            catch (Exception)
            {

                ciudadVm.Paises = _servicios.GetPaisesDropDownList();
                ModelState.AddModelError(string.Empty, "Error al intentar agregar una Ciudad");
                return View(ciudadVm);

            }
        }

        public ActionResult DeleteCity(int? id) {

            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var ciudad = _serviciosCiudades.GetCiudadPorId(id.Value);
            if (ciudad == null)
            {
                return HttpNotFound("Código de ciudad inesistente!!!");
            }
            var ciudadVm = _mapper.Map<CiudadEditVm>(ciudad);
            ciudadVm.Paises = _servicios.GetPaisesDropDownList();
            return View(ciudadVm);

        }
        [HttpPost, ActionName("DeleteCity")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCityConfirm(int id)
        {
            var ciudad = _serviciosCiudades.GetCiudadPorId(id);
            var ciudadVm = _mapper.Map<CiudadEditVm>(ciudad);
            try
            {
                if (!_serviciosCiudades.EstaRelacionada(ciudad))
                {
                    _serviciosCiudades.Borrar(id);
                    return RedirectToAction($"Details/{ciudad.PaisId}");
                }
                else
                {
                    ciudadVm.Paises = _servicios.GetPaisesDropDownList();
                    ModelState.AddModelError(string.Empty, "Ciudad relacionada... Baja denegada");
                    return View(ciudadVm);
                }
            }
            catch (Exception)
            {

                ciudadVm.Paises = _servicios.GetPaisesDropDownList();
                ModelState.AddModelError(string.Empty, "Error al intentar borrar una ciudad");
                return View(ciudadVm);

            }
        }

        public ActionResult EditCity(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var ciudad = _serviciosCiudades.GetCiudadPorId(id.Value);
            if (ciudad == null)
            {
                return HttpNotFound("Código de ciudad inesistente!!!");
            }
            var ciudadVm = _mapper.Map<CiudadEditVm>(ciudad);
            ciudadVm.PaisAnteriorId = ciudad.PaisId;
            ciudadVm.Paises = _servicios.GetPaisesDropDownList();
            return View(ciudadVm);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCity(CiudadEditVm ciudadVm)
        {
            if (!ModelState.IsValid)
            {
                ciudadVm.Paises = _servicios.GetPaisesDropDownList();
                return View(ciudadVm);

            }
            try
            {
                var ciudad = _mapper.Map<Ciudad>(ciudadVm);
                if (!_serviciosCiudades.Existe(ciudad))
                {
                    _serviciosCiudades.Guardar(ciudad);
                    return RedirectToAction($"Details/{ciudadVm.PaisAnteriorId}");
                }
                else
                {
                    ciudadVm.Paises = _servicios.GetPaisesDropDownList();
                    ModelState.AddModelError(string.Empty, "Ciudad existente!!!");
                    return View(ciudadVm);

                }
            }
            catch (Exception)
            {

                ciudadVm.Paises = _servicios.GetPaisesDropDownList();
                ModelState.AddModelError(string.Empty, "Error al intentar agregar una ciudad");
                return View(ciudadVm);
            }
        }
    }
}