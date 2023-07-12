using AutoMapper;
using System.Collections.Generic;
using System.Web.Mvc;
using TiendaVirtual.Servicios.Interfaces;
using TiendaVirtual.Web.App_Start;
using TiendaVirtual.Web.ViewModels.Ciudad;

namespace TiendaVirtual.Web.Controllers
{
    public class GenericoController : Controller
    {
        // GET: Generico
        private readonly IServiciosCiudades _serviciosCiudades;
        private readonly IMapper _mapper;
        public GenericoController(IServiciosCiudades serviciosCiudades)
        {
            _serviciosCiudades = serviciosCiudades;
            _mapper = AutoMapperConfig.Mapper;
        }
        public JsonResult GetCities(int paisId)
        {
            var lista = _serviciosCiudades.GetCiudades(paisId);
            var ciudadesVm = _mapper.Map<List<CiudadListVm>>(lista);
            return Json(ciudadesVm);
        }
    }
}