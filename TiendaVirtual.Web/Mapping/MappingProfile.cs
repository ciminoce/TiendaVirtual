using AutoMapper;
using System;
using TiendaVirtual.Entidades.Dtos.Categoria;
using TiendaVirtual.Entidades.Dtos.Ciudad;
using TiendaVirtual.Entidades.Dtos.Cliente;
using TiendaVirtual.Entidades.Dtos.DetalleVenta;
using TiendaVirtual.Entidades.Dtos.Producto;
using TiendaVirtual.Entidades.Dtos.Proveedor;
using TiendaVirtual.Entidades.Dtos.Venta;
using TiendaVirtual.Entidades.Entidades;
using TiendaVirtual.Web.Models.Carrito;
using TiendaVirtual.Web.ViewModels.Carrito;
using TiendaVirtual.Web.ViewModels.Categoria;
using TiendaVirtual.Web.ViewModels.Ciudad;
using TiendaVirtual.Web.ViewModels.Cliente;
using TiendaVirtual.Web.ViewModels.DetalleVenta;
using TiendaVirtual.Web.ViewModels.Pais;
using TiendaVirtual.Web.ViewModels.Producto;
using TiendaVirtual.Web.ViewModels.Proveedor;
using TiendaVirtual.Web.ViewModels.Venta;

namespace TiendaVirtual.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            LoadPaisesMapping();
            LoadCiudadesMapping();
            LoadClientesMapping();
            LoadProveedoresMapping();
            LoadProductosMapping();
            LoadCategoriasMapping();
            LoadVentasMapping();
            LoadDetalleVentaMapping();
            LoadCarritoMapping();
        }

        private void LoadCarritoMapping()
        {
            CreateMap<ItemCarrito, ItemCarritoVm>();
        }

        private void LoadDetalleVentaMapping()
        {
            CreateMap<DetalleVentaListDto, DetalleVentaListVm>();
        }

        private void LoadVentasMapping()
        {
            CreateMap<VentaListDto, VentaListVm>();
        }

        private void LoadCategoriasMapping()
        {
            CreateMap<CategoriaListDto, CategoriaListVm>();
            CreateMap<Categoria, CategoriaEditVm>().ReverseMap();
            CreateMap<Categoria, CategoriaListVm>();

        }

        private void LoadProductosMapping()
        {
            CreateMap<ProductoListDto, ProductoListVm>();
            CreateMap<ProductoEditVm,Producto>().ReverseMap();
            CreateMap<Producto, ProductoListVm>()
                .ForMember(dest=>dest.Categoria,
                opt=>opt.MapFrom(src=>src.Categoria.NombreCategoria));
        }

        private void LoadProveedoresMapping()
        {
            CreateMap<ProveedorListDto, ProveedorListVm>();
            CreateMap<Proveedor, ProveedorEditVm>().ReverseMap();
            CreateMap<Proveedor, ProveedorListVm>()
                .ForMember(dest => dest.ProveedorId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.NombreProveedor, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Pais, opt => opt.MapFrom(src => src.Pais.NombrePais))
                .ForMember(dest => dest.Ciudad, opt => opt.MapFrom(src => src.Ciudad.NombreCiudad));

        }

        private void LoadClientesMapping()
        {
            CreateMap<ClienteListDto, ClienteListVm>();
            CreateMap<Cliente, ClienteEditVm>()
                .ForMember(dest => dest.EmailAnterior, opt => opt.MapFrom(src => src.Email));
            CreateMap<ClienteEditVm, Cliente>();
            CreateMap<Cliente, ClienteListVm>()
                .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.NombreCliente, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Pais, opt => opt.MapFrom(src => src.Pais.NombrePais))
                .ForMember(dest => dest.Ciudad, opt => opt.MapFrom(src => src.Ciudad.NombreCiudad));
        }

        private void LoadCiudadesMapping()
        {
            CreateMap<CiudadListDto, CiudadListVm>();
            CreateMap<CiudadEditVm, Ciudad>().ReverseMap();
            CreateMap<Ciudad, CiudadListVm>()
                .ForMember(dest => dest.NombrePais,
                opt => opt.MapFrom(src => src.Pais.NombrePais));

        }

        private void LoadPaisesMapping()
        {
            CreateMap<Pais, PaisListVm>();
            CreateMap<Pais, PaisEditVm>().ReverseMap();

        }
    }
}