using AutoMapper;
using System.Collections.Generic;
using System.Web.Mvc;
using TiendaVirtual.Entidades.Entidades;
using TiendaVirtual.Servicios.Interfaces;
using TiendaVirtual.Web.App_Start;
using TiendaVirtual.Web.Models.Carrito;
using TiendaVirtual.Web.ViewModels.Carrito;

namespace TiendaVirtual.Web.Controllers
{
    public class CarritoController : Controller
    {
        // GET: Carrito
        private readonly IServiciosCarrito _serviciosCarritos;
        private readonly IServiciosProductos _serviciosProductos;
        private readonly IMapper _mapper;
        public CarritoController(IServiciosProductos serviciosProductos,
            IServiciosCarrito serviciosCarritos)
        {
            _serviciosProductos = serviciosProductos;
            _mapper = AutoMapperConfig.Mapper;
            _serviciosCarritos = serviciosCarritos;
        }

        private Carrito _carrito;

        public ActionResult Index(string returnUrl)
        {
            _carrito = GetCarrito();
            if (_carrito.GetCantidad() == 0)
            {
                return View("EmptyCart");
            }
            var listaItem = _carrito.GetItems();
            var listaItemVm = _mapper.Map<List<ItemCarritoVm>>(listaItem);

            CarritoVm carritoVm = new CarritoVm
            {
                Items = listaItemVm,
                returnUrl=returnUrl
            };
            return View(carritoVm);
        }
        public ActionResult MostrarCarrito()
        {
            _carrito = GetCarrito();

            return PartialView("_MostrarCarrito", _carrito);
        }

        private Carrito GetCarrito()
        {
            if (_carrito != null)
            {
                return _carrito;
            }
            _carrito = (Carrito)Session["carrito"];
            if (_carrito == null)
            {
                _carrito = new Carrito();
                Session["carrito"] = _carrito;
            }

            if (_carrito.GetCantidad()==0)
            {
                if (_serviciosCarritos.GetCantidad(User.Identity.Name) > 0)
                {
                    var listaItemsCarrito = _serviciosCarritos
                        .GetCarrito(User.Identity.Name);
                    foreach (var item in listaItemsCarrito)
                    {
                        _carrito.AddToCart(item);
                    }
                    Session["carrito"] = _carrito;
                }

            }
            return _carrito;
        }

        [HttpPost]
        public ActionResult AddToCart(int productoId, string returnUrl, int cantidad = 1)
        {
            var productoDto = _serviciosProductos.GetProductoPorId(productoId);
            if (productoDto.Stock >= cantidad)
            {
                ItemCarrito itemCarrito = new ItemCarrito
                {
                    ProductoId = productoDto.ProductoId,
                    UserName=User.Identity.Name,
                    NombreProducto = productoDto.NombreProducto,
                    PrecioUnitario = productoDto.PrecioUnitario,
                    Cantidad = cantidad,

                };
                _carrito = GetCarrito();
                _carrito.AddToCart(itemCarrito);
                Session["carrito"] = _carrito;
                _serviciosCarritos.Guardar(itemCarrito);
                return RedirectToAction("Index",new {returnUrl});

            }
            else
            {
                TempData["Error"] = "Cantidad mayor al stock";
                return RedirectToAction("Index",new {returnUrl});
            }
        }

        //public ActionResult RemoveFromCart(int productoId, string confirmarBorrado, string returnUrl)
        //{
        //    if (confirmarBorrado=="SI")
        //    {
        //        _carrito = GetCarrito();
        //        _carrito.RemoveFromCart(productoId);
        //        Session["carrito"] = _carrito;

        //    }
        //    return RedirectToAction("Index", new { returnUrl });

        //}
        public ActionResult RemoveFromCart(int productoId, string returnUrl)
        {
            _carrito = GetCarrito();
            _carrito.RemoveFromCart(productoId);
            Session["carrito"] = _carrito;
            _serviciosCarritos.Borrar(User.Identity.Name, productoId);
            return RedirectToAction("Index", new { returnUrl });

        }

        public ActionResult CancelOrder()
        {
            _carrito = GetCarrito();
            _carrito.Clear();
            Session["carrito"] = _carrito;
            return View();
        }
    }
}