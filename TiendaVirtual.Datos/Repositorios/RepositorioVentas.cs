using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TiendaVirtual.Datos;
using TiendaVirtual.Datos.Interfaces;
using TiendaVirtual.Entidades.Dtos.Venta;
using TiendaVirtual.Entidades.Entidades;

namespace TiendaVirtual.Datos.Repositorios
{
    public class RepositorioVentas : IRepositorioVentas
    {
        private readonly NeptunoDbContext _context;

        public RepositorioVentas(NeptunoDbContext context)
        {
            _context = context;
        }

        public void Agregar(Venta venta)
        {
            _context.Ventas.Add(venta);
        }

        public List<VentaListDto> Filtrar(Func<Venta, bool> predicado)
        {
            throw new NotImplementedException();
        }

        public int GetCantidad()
        {
            return _context.Ventas.Count();
        }

        public VentaListDto GetVentaPorId(int id)
        {
            return _context.Ventas
                .Include(v => v.Cliente)
    
                .Select(v => new VentaListDto
                {
                    VentaId = v.VentaId,
                    FechaVenta = v.FechaVenta,
                    Cliente = v.Cliente.Nombre,
                    Total = v.Total,
                    Estado = v.Estado.ToString()
                }).SingleOrDefault(v=>v.VentaId== id);

        }

        public List<VentaListDto> GetVentas()
        {
            return _context.Ventas
                .Include(v => v.Cliente)
                .OrderBy(v => v.FechaVenta)
                .Select(v => new VentaListDto
                {
                    VentaId = v.VentaId,
                    FechaVenta = v.FechaVenta,
                    Cliente = v.Cliente.Nombre,
                    Total = v.Total,
                    Estado = v.Estado.ToString()
                }).ToList();
        }
    }
}
