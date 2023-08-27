using System.Web.Mvc;
using TiendaVirtual.Servicios.Interfaces;

namespace TiendaVirtual.Web.Controllers
{
    public class HomeController : Controller
    {
        //TODO: Modificar el index
        private readonly IServiciosCategorias _serviciosCategorias;
        private readonly IServiciosClientes _serviciosClientes;
        private readonly IServiciosProductos _serviciosProductos;
        private readonly IServiciosVentas _serviciosVentas;
        public HomeController(IServiciosCategorias serviciosCategorias,
            IServiciosClientes serviciosClientes,
            IServiciosProductos serviciosProductos,
            IServiciosVentas serviciosVentas)
        {
            _serviciosCategorias = serviciosCategorias;
            _serviciosClientes= serviciosClientes;
            _serviciosProductos= serviciosProductos;
            _serviciosVentas= serviciosVentas;
        }
        public ActionResult Index()
        {
            var cantidadCategorias = _serviciosCategorias.GetCantidad();
            var cantidadProductos = _serviciosProductos.GetCantidad();
            var cantidadClientes=_serviciosClientes.GetCantidad();
            var cantidadVentas = _serviciosVentas.GetCantidad();

            ViewBag.cantidadCategorias = cantidadCategorias;
            ViewBag.cantidadProductos=cantidadProductos;
            ViewBag.cantidadClientes=cantidadClientes;
            ViewBag.cantidadVentas = cantidadVentas;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}