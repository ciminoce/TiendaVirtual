using AutoMapper;
using System.Collections.Generic;
using System.Web.Mvc;
using TiendaVirtual.Entidades.Entidades;
using TiendaVirtual.Servicios.Interfaces;
using TiendaVirtual.Web.App_Start;
using TiendaVirtual.Web.ViewModels.Ciudad;
using TiendaVirtual.Web.ViewModels.Proveedor;

namespace TiendaVirtual.Web.Controllers
{
    public class ProveedoresController : Controller
    {
        private readonly IServiciosProveedores _servicios;
        private readonly IServiciosPaises _serviciosPaises;
        private readonly IServiciosCiudades _serviciosCiudades;
        private readonly IMapper _mapper;
        public ProveedoresController(IServiciosProveedores servicios,
            IServiciosPaises serviciosPaises,
            IServiciosCiudades serviciosCiudades)
        {
            _servicios = servicios;
            _serviciosPaises = serviciosPaises;
            _serviciosCiudades = serviciosCiudades;
            _mapper = AutoMapperConfig.Mapper;
        }
        // GET: Proveedors
        public ActionResult Index()
        {
            var lista = _servicios.GetProveedores();
            var listaVm = _mapper.Map<List<ProveedorListVm>>(lista);
            return View(listaVm);
        }

        public ActionResult Create()
        {
            var proveedorVm = new ProveedorEditVm
            {
                Paises = _serviciosPaises.GetPaisesDropDownList(),
                Ciudades = new List<SelectListItem>()

            };
            return View(proveedorVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProveedorEditVm proveedorVm)
        {
            if (!ModelState.IsValid)
            {
                proveedorVm.Paises = _serviciosPaises.GetPaisesDropDownList();
                proveedorVm.Ciudades = _serviciosCiudades.GetCiudadesDropDownList(proveedorVm.PaisId);
                return View(proveedorVm);
            }
            try
            {
                var proveedor = _mapper.Map<Proveedor>(proveedorVm);
                if (!_servicios.Existe(proveedor))
                {
                    _servicios.Guardar(proveedor);
                    TempData["Msg"] = "Registro agregado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Proveedor existente!!!");
                    proveedorVm.Paises = _serviciosPaises.GetPaisesDropDownList();
                    proveedorVm.Ciudades = _serviciosCiudades.GetCiudadesDropDownList(proveedorVm.PaisId);

                    return View(proveedorVm);
                }
            }
            catch (System.Exception)
            {

                ModelState.AddModelError(string.Empty, "Error al intentar agregar un Proveedor");
                proveedorVm.Paises = _serviciosPaises.GetPaisesDropDownList();
                proveedorVm.Ciudades = _serviciosCiudades.GetCiudadesDropDownList(proveedorVm.PaisId);

                return View(proveedorVm);
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var proveedor = _servicios.GetProveedorPorId(id.Value);
            if (proveedor == null)
            {
                return HttpNotFound("Cód. de proveedor inexistente!!!");
            }
            var proveedorVm = _mapper.Map<ProveedorEditVm>(proveedor);
            proveedorVm.Paises = _serviciosPaises.GetPaisesDropDownList();
            proveedorVm.Ciudades = _serviciosCiudades.GetCiudadesDropDownList(proveedor.PaisId);
            return View(proveedorVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProveedorEditVm proveedorVm)
        {
            if (!ModelState.IsValid)
            {
                proveedorVm.Paises = _serviciosPaises.GetPaisesDropDownList();
                proveedorVm.Ciudades = _serviciosCiudades.GetCiudadesDropDownList(proveedorVm.PaisId);
                return View(proveedorVm);

            }
            try
            {
                var proveedor = _mapper.Map<Proveedor>(proveedorVm);
                if (!_servicios.Existe(proveedor))
                {
                    _servicios.Guardar(proveedor);
                    TempData["Msg"] = "Registro editado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Proveedor existente!!!");
                    proveedorVm.Paises = _serviciosPaises.GetPaisesDropDownList();
                    proveedorVm.Ciudades = _serviciosCiudades.GetCiudadesDropDownList(proveedorVm.PaisId);
                    return View(proveedorVm);

                }
            }
            catch (System.Exception)
            {
                ModelState.AddModelError(string.Empty, "Proveedor existente!!!");
                proveedorVm.Paises = _serviciosPaises.GetPaisesDropDownList();
                proveedorVm.Ciudades = _serviciosCiudades.GetCiudadesDropDownList(proveedorVm.PaisId);
                return View(proveedorVm);
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var proveedor = _servicios.GetProveedorPorId(id.Value);
            if (proveedor == null)
            {
                return HttpNotFound("Cód. proveedor inexistente!!!");
            }
            var proveedorVm = _mapper.Map<ProveedorListVm>(proveedor);
            return View(proveedorVm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            var proveedor = _servicios.GetProveedorPorId(id);
            var proveedorVm = _mapper.Map<ProveedorListVm>(proveedor);
            try
            {
                if (!_servicios.EstaRelacionado(proveedor))
                {
                    _servicios.Borrar(id);
                    TempData["Msg"] = "Proveedor borrado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Registro relacionado... Baja denegada");
                    return View(proveedorVm);
                }
            }
            catch (System.Exception)
            {

                ModelState.AddModelError(string.Empty, "Error al intengar borrar un proveedor");
                return View(proveedorVm);

            }
        }

    }
}