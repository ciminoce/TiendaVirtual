using AutoMapper;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using TiendaVirtual.Entidades.Entidades;
using TiendaVirtual.Servicios.Interfaces;
using TiendaVirtual.Web.App_Start;
using TiendaVirtual.Web.Helpers;
using TiendaVirtual.Web.ViewModels.Ciudad;
using TiendaVirtual.Web.ViewModels.Cliente;

namespace TiendaVirtual.Web.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IServiciosClientes _servicios;
        private readonly IServiciosPaises _serviciosPaises;
        private readonly IServiciosCiudades _serviciosCiudades;
        private readonly IMapper _mapper;
        public ClientesController(IServiciosClientes servicios,
            IServiciosPaises serviciosPaises,
            IServiciosCiudades serviciosCiudades)
        {
            _servicios = servicios;
            _serviciosPaises= serviciosPaises;
            _serviciosCiudades= serviciosCiudades;
            _mapper = AutoMapperConfig.Mapper;
        }
        // GET: Clientes
        public ActionResult Index(int? page, int? pageSize, string SortBy="Cliente", string SearchBy=null)
        {
            page= page ?? 1;
            pageSize= pageSize ?? 10;
            var lista=_servicios.GetClientes();
            if (SearchBy!=null)
            {
                lista=lista
                    .Where(c=>c.NombreCliente.Contains(SearchBy)|| c.Pais.Contains(SearchBy))
                    .ToList();
            }

            var listaVm = _mapper.Map<List<ClienteListVm>>(lista);
            if (SortBy == "Cliente")
            {
                listaVm=listaVm.OrderBy(c=>c.NombreCliente).ToList();
            }else
            {
                listaVm=listaVm.OrderBy(c=>c.Pais).ThenBy(c=>c.Ciudad).ToList();
            }
            var clienteVm = new ClienteListSortVm
            {
                Clientes = listaVm.ToPagedList(page.Value, pageSize.Value),
                Sorts = new Dictionary<string, string> {
                    {"Por Cliente","Cliente"},
                    {"Por País","Pais" }
                },
                SortBy = SortBy,
                SearchBy = SearchBy
            };

            return View(clienteVm);
        }

        public ActionResult Create()
        {
            var clienteVm = new ClienteEditVm
            {
                Paises = _serviciosPaises.GetPaisesDropDownList(),
                Ciudades = new List<SelectListItem>()

            };
            return View(clienteVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClienteEditVm clienteVm)
        {
            if (!ModelState.IsValid)
            {
                clienteVm.Paises = _serviciosPaises.GetPaisesDropDownList();
                clienteVm.Ciudades = _serviciosCiudades.GetCiudadesDropDownList(clienteVm.PaisId);
                return View(clienteVm);
            }
            try
            {
                var cliente = _mapper.Map<Cliente>(clienteVm);
                if (!_servicios.Existe(cliente))
                {
                    using(var tran=new TransactionScope())
                    {
                        _servicios.Guardar(cliente);
                        UsersHelper.CreateUserAsp(cliente.Email, "Cliente");
                        tran.Complete();
                    }
                    TempData["Msg"] = "Registro agregado satisfactoriamente";
                    return RedirectToAction("Index");

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Cliente existente!!!");
                    clienteVm.Paises = _serviciosPaises.GetPaisesDropDownList();
                    clienteVm.Ciudades = _serviciosCiudades.GetCiudadesDropDownList(clienteVm.PaisId);

                    return View(clienteVm);
                }
            }
            catch (System.Exception)
            {

                ModelState.AddModelError(string.Empty, "Error al intentar agregar un Cliente");
                clienteVm.Paises = _serviciosPaises.GetPaisesDropDownList();
                clienteVm.Ciudades = _serviciosCiudades.GetCiudadesDropDownList(clienteVm.PaisId);

                return View(clienteVm);
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var cliente = _servicios.GetClientePorId(id.Value);
            if (cliente==null)
            {
                return HttpNotFound("Cód. de cliente inexistente!!!");
            }
            var clienteVm = _mapper.Map<ClienteEditVm>(cliente);
            clienteVm.Paises = _serviciosPaises.GetPaisesDropDownList();
            clienteVm.Ciudades = _serviciosCiudades.GetCiudadesDropDownList(cliente.PaisId);
            return View(clienteVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClienteEditVm clienteVm)
        {
            if (!ModelState.IsValid)
            {
                clienteVm.Paises = _serviciosPaises.GetPaisesDropDownList();
                clienteVm.Ciudades = _serviciosCiudades.GetCiudadesDropDownList(clienteVm.PaisId);
                return View(clienteVm);

            }
            try
            {
                var cliente = _mapper.Map<Cliente>(clienteVm);
                if (!_servicios.Existe(cliente))
                {
                    using(var tran=new TransactionScope())
                    {
                        _servicios.Guardar(cliente);
                        //Preguntar que paso
                        UsersHelper.UpdateUserName(clienteVm.EmailAnterior, cliente.Email);
                        tran.Complete();
                    }
                    TempData["Msg"] = "Registro editado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Cliente existente!!!");
                    clienteVm.Paises = _serviciosPaises.GetPaisesDropDownList();
                    clienteVm.Ciudades = _serviciosCiudades.GetCiudadesDropDownList(clienteVm.PaisId);
                    return View(clienteVm);

                }
            }
            catch (System.Exception)
            {
                ModelState.AddModelError(string.Empty, "Cliente existente!!!");
                clienteVm.Paises = _serviciosPaises.GetPaisesDropDownList();
                clienteVm.Ciudades = _serviciosCiudades.GetCiudadesDropDownList(clienteVm.PaisId);
                return View(clienteVm);
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var cliente = _servicios.GetClientePorId(id.Value);
            if (cliente==null)
            {
                return HttpNotFound("Cód. cliente inexistente!!!");
            }
            var clienteVm=_mapper.Map<ClienteListVm>(cliente);
            return View(clienteVm);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            var cliente = _servicios.GetClientePorId(id);
            var clienteVm = _mapper.Map<ClienteListVm>(cliente);
            try
            {
                if (!_servicios.EstaRelacionado(cliente))
                {
                    _servicios.Borrar(id);
                    TempData["Msg"] = "Cliente borrado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Registro relacionado... Baja denegada");
                    return View(clienteVm);
                }
            }
            catch (System.Exception)
            {

                ModelState.AddModelError(string.Empty, "Error al intengar borrar un cliente");
                return View(clienteVm);

            }
        }

    }
}