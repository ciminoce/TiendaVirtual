using AutoMapper;
using PagedList;
using System.Collections.Generic;
using System.Web.Mvc;
using TiendaVirtual.Entidades.Entidades;
using TiendaVirtual.Servicios.Interfaces;
using TiendaVirtual.Web.App_Start;
using TiendaVirtual.Web.ViewModels.Categoria;

namespace TiendaVirtual.Web.Controllers
{
    public class CategoriasController : Controller
    {
        // GET: Paises
        private readonly IServiciosCategorias _servicios;
        private readonly IServiciosProductos _serviciosProductos;
        private readonly IMapper _mapper;
        public CategoriasController(IServiciosCategorias servicios, IServiciosProductos serviciosProductos)
        {
            _servicios = servicios;
            _mapper = AutoMapperConfig.Mapper;
            _serviciosProductos = serviciosProductos;
        }
        public ActionResult Index(int? page, int? pageSize)
        {
            var lista = _servicios.GetCategorias();
            //var lista = new List<Categoria>();
            //var listaVm=GetListaPaisesLstVm(lista);
            var listaVm = _mapper.Map<List<CategoriaListVm>>(lista);
            listaVm.ForEach(c => c.CantidadProductos = _serviciosProductos
                    .GetCantidad(p => p.CategoriaId == c.CategoriaId));
            page = page ?? 1;
            pageSize = pageSize ?? 10;
            ViewBag.PageSize = pageSize;
            if (User.IsInRole("Admin"))
            {
                return View(listaVm.ToPagedList(page.Value, pageSize.Value));

            }
            return View("ReadOnlyIndex",listaVm.ToPagedList(page.Value, pageSize.Value));
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoriaEditVm categoriaVm)
        {
            if (ModelState.IsValid)
            {
                var Categoria = _mapper.Map<Categoria>(categoriaVm);
                if (_servicios.Existe(Categoria))
                {
                    ModelState.AddModelError(string.Empty, "Categoría existente!!!");
                    return View(Categoria);
                }
                _servicios.Guardar(Categoria);
                TempData["Msg"] = "Registro guardado satisfactoriamente";
                return RedirectToAction("Index");

            }
            else
            {
                return View(categoriaVm);
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var categoria = _servicios.GetCategoriaPorId(id.Value);
            if (categoria == null)
            {
                return HttpNotFound("Código de país inesistente!!!");
            }
            var categoriaVm = _mapper.Map<CategoriaListVm>(categoria);
            return View(categoriaVm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfim(int id)
        {
            var categoria = _servicios.GetCategoriaPorId(id);
            if (_servicios.EstaRelacionado(categoria))
            {
                var categoriaVm = _mapper.Map<CategoriaListVm>(categoria);
                ModelState.AddModelError(string.Empty, "Categoría relacionada... Baja denegada!!!");
                return View(categoriaVm);

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
            var categoria = _servicios.GetCategoriaPorId(id.Value);
            if (categoria == null)
            {
                return HttpNotFound("Código de país inesistente!!!");
            }
            var categoriaVm = _mapper.Map<CategoriaEditVm>(categoria);
            return View(categoriaVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoriaEditVm categoriaVm)
        {
            if (!ModelState.IsValid)
            {
                return View(categoriaVm);
            }
            var categoria = _mapper.Map<Categoria>(categoriaVm);
            if (_servicios.Existe(categoria))
            {
                ModelState.AddModelError(string.Empty, "País existente!!!");
                return View(categoriaVm);
            }
            _servicios.Guardar(categoria);
            TempData["Msg"] = "Registro editado satisfactoriamente!!!";
            return RedirectToAction("Index");

        }
        //TODO: Hacer detalles de categorías
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var categoria = _servicios.GetCategoriaPorId(id.Value);
            if (categoria == null)
            {
                return HttpNotFound("Código de categoría inesistente!!!");
            }
            var categoriaVm = _mapper.Map<CategoriaListVm>(categoria);
            categoriaVm.CantidadProductos = _serviciosProductos
                    .GetCantidad(p => p.CategoriaId == categoria.CategoriaId);
            //TODO: Hacer el manejo del detalle de categoría
            var categoriaDetailVm = new CategoriaDetailVm()
            {
                //Categorias = categoriaVm,
                //Ciudades = _mapper.Map<List<CiudadListVm>>(
                //_serviciosCiudades.GetCiudades(Categoria.PaisId))
            };
            return View(categoriaDetailVm);
        }
       
    }
}