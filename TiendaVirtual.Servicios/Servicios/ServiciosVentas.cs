using System;
using System.Collections.Generic;
using System.Transactions;
using TiendaVirtual.Datos;
using TiendaVirtual.Datos.Interfaces;
using TiendaVirtual.Entidades.Dtos.DetalleVenta;
using TiendaVirtual.Entidades.Dtos.Venta;
using TiendaVirtual.Entidades.Entidades;
using TiendaVirtual.Entidades.Enums;
using TiendaVirtual.Servicios.Interfaces;

namespace Neptuno2022EF.Servicios.Servicios
{
    public class ServiciosVentas : IServiciosVentas
    {
        private readonly IRepositorioVentas _repositorio;
        private readonly IRepositorioDetalleVentas _repoDetalleVentas;
        private readonly IRepositorioProductos _repoProductos;
        private readonly IRepositorioCarritos _repoCarritos;
        private readonly IUnitOfWork _unitOfWork;
        
        public ServiciosVentas(IRepositorioVentas repositorio,
            IRepositorioDetalleVentas repoDetalleVentas,
            IRepositorioProductos repoProductos,
            IRepositorioCarritos repoCarritos,
            IUnitOfWork unitOfWork)
        {
            _repositorio = repositorio;
            _repoDetalleVentas = repoDetalleVentas;
            _repoProductos = repoProductos;
            _repoCarritos = repoCarritos;
            _unitOfWork = unitOfWork;
        }

        public int GetCantidad()
        {
            try
            {
                return _repositorio.GetCantidad();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<DetalleVentaListDto> GetDetalleVenta(int ventaId)
        {
            try
            {
                return _repoDetalleVentas.GetDetalleVentas(ventaId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<VentaListDto> GetVentas()
        {
            try
            {
                return _repositorio.GetVentas();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public VentaListDto GetVentasPorId(int id)
        {
            try
            {
                return _repositorio.GetVentaPorId(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Guardar(Venta venta, string user)
        {
            try
            {
                using (var transaction=new TransactionScope())
                {

                    var ventaGuardar = new Venta()
                    {
                        ClienteId = venta.ClienteId,
                        FechaVenta = venta.FechaVenta,
                        TransaccionId = venta.TransaccionId,
                        Estado=Estado.Paga,
                        Total = venta.Total
                    };
                    _repositorio.Agregar(ventaGuardar);
                    _unitOfWork.SaveChanges();
                    foreach (var item in venta.Detalles)
                    {
                        item.VentaId = ventaGuardar.VentaId;
                        _repoDetalleVentas.Agregar(item);
                        _repoProductos.ActualizarStock(item.ProductoId, item.Cantidad);
                        _repoCarritos.Borrar(user, item.ProductoId);
                    }
                    _unitOfWork.SaveChanges();
                    transaction.Complete();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
