using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using TiendaVirtual.Datos.Interfaces;
using TiendaVirtual.Entidades.Dtos.DetalleVenta;
using TiendaVirtual.Entidades.Entidades;

namespace TiendaVirtual.Datos.Repositorios
{
    public class RepositorioDetalleVentas : IRepositorioDetalleVentas
    {
        private readonly NeptunoDbContext _context;
        public RepositorioDetalleVentas(NeptunoDbContext context)
        {
            _context = context;
        }
        public void Agregar(DetalleVenta detalle)
        {
            _context.DetalleVentas.Add(detalle);
        }

        public void Borrar(int id)
        {
            throw new NotImplementedException();
        }

        public void Editar(DetalleVenta detalle)
        {
            throw new NotImplementedException();
        }

        public bool EstaRelacionado(DetalleVenta detalle)
        {
            throw new NotImplementedException();
        }

        public bool Existe(DetalleVenta detalle)
        {
            throw new NotImplementedException();
        }

        public DetalleVenta GetDetalleVentaPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<DetalleVentaListDto> GetDetalleVentas(int ventaId)
        {
            return _context.DetalleVentas.Include(d => d.Producto)
                .Where(d => d.VentaId == ventaId)
                .Select(d => new DetalleVentaListDto()
                {
                    DetalleVentaId = d.DetalleVentaId,
                    Producto = d.Producto.NombreProducto,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,

                }).ToList();
        }
    }
}
