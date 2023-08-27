using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TiendaVirtual.Datos.Interfaces;
using TiendaVirtual.Entidades.Dtos.Producto;
using TiendaVirtual.Entidades.Entidades;

namespace TiendaVirtual.Datos.Repositorios
{
    public class RepositorioProductos : IRepositorioProductos
    {
        private readonly NeptunoDbContext _context;
        public RepositorioProductos(NeptunoDbContext context)
        {
            _context = context;
        }

        public void ActualizarStock(int productoId, int cantidad)
        {
            var productoInDb = _context.Productos.SingleOrDefault(p => p.ProductoId == productoId);
            productoInDb.UnidadesEnPedido -= cantidad;
            productoInDb.Stock -= cantidad;
            _context.Entry(productoInDb).State = EntityState.Modified;

        }
        public void Agregar(Producto producto)
        {
            _context.Productos.Add(producto);
        }

        public void ActualizarUnidadesEnPedido(int productoId, int cantidad)
        {
            var productoInDb = _context.Productos.SingleOrDefault(p => p.ProductoId == productoId);
            productoInDb.UnidadesEnPedido += cantidad;
            _context.Entry(productoInDb).State = EntityState.Modified;
        }


        public void Borrar(int id)
        {
            var productoInDb = _context.Productos.SingleOrDefault(p => p.ProductoId == id);
            if (productoInDb == null)
            {
                throw new Exception("Producto borrado por otro usuario");
            }
            _context.Entry(productoInDb).State = EntityState.Deleted;
        }

        public void Editar(Producto producto)
        {
            var productoInDb = _context.Productos.SingleOrDefault(p => p.ProductoId == producto.ProductoId);
            if (productoInDb == null)
            {
                throw new Exception("Producto borrado por otro usuario");
            }
            productoInDb.NombreProducto = producto.NombreProducto;
            productoInDb.CategoriaId = producto.CategoriaId;
            productoInDb.ProveedorId = producto.ProveedorId;
            productoInDb.StockMinimo = producto.StockMinimo;
            productoInDb.Stock = producto.Stock;
            productoInDb.StockMinimo = producto.StockMinimo;
            productoInDb.PrecioUnitario = producto.PrecioUnitario;
            productoInDb.Imagen = producto.Imagen;
            productoInDb.Suspendido = producto.Suspendido;
        }


        public bool EstaRelacionado(Producto producto)
        {
            return false;
        }


        public bool Existe(Producto producto)
        {
            if (producto.ProductoId == 0)
            {
                return _context.Productos.Any(p => p.NombreProducto == producto.NombreProducto &&
                p.CategoriaId == producto.CategoriaId);
            }
            return _context.Productos.Any(p => p.NombreProducto == producto.NombreProducto &&
                p.CategoriaId == producto.CategoriaId && p.ProductoId != producto.ProductoId);

        }


        public List<ProductoListDto> Filtrar(Func<Producto, bool> predicado)
        {
            return _context.Productos.Include(p => p.Categoria).Where(predicado).Select(p => new ProductoListDto
            {
                ProductoId = p.ProductoId,
                NombreProducto = p.NombreProducto,
                Categoria = p.Categoria.NombreCategoria,
                PrecioUnitario = p.PrecioUnitario,
                Imagen = p.Imagen,
                Suspendido = p.Suspendido,
                UnidadesDisponibles = p.UnidadesDisponibles()
            })
                .ToList();
        }


        public Producto GetProductoPorId(int id)
        {
            return _context.Productos.Include(p => p.Categoria)
                .Include(p => p.Proveedor).SingleOrDefault(p => p.ProductoId == id);
        }

        public List<ProductoListDto> GetProductos(bool todos)
        {
            IQueryable<ProductoListDto> query = _context.Productos.Include(p => p.Categoria)
                .Select(p => new ProductoListDto()
                {
                    ProductoId = p.ProductoId,
                    NombreProducto = p.NombreProducto,
                    Categoria = p.Categoria.NombreCategoria,
                    PrecioUnitario = p.PrecioUnitario,
                    UnidadesDisponibles = p.Stock - p.UnidadesEnPedido,
                    Suspendido = p.Suspendido,
                    Imagen = p.Imagen
                });
            if (!todos)
            {
                query = query.Where(p => (p.UnidadesDisponibles > 0 && p.Suspendido == false) || p.Suspendido == false);
            }
            return query.ToList();
            //return _context.Productos.Include(p => p.Categoria)
            //    .Select(p => new ProductoListDto()
            //    {
            //        ProductoId = p.ProductoId,
            //        NombreProducto = p.NombreProducto,
            //        Categoria = p.Categoria.NombreCategoria,
            //        PrecioUnitario = p.PrecioUnitario,
            //        UnidadesDisponibles = p.Stock - p.UnidadesEnPedido,
            //        Suspendido = p.Suspendido,
            //        Imagen = p.Imagen
            //    }).ToList();
        }

        public List<ProductoListDto> GetProductos(int categoriaId)
        {
            return _context.Productos.Include(p => p.Categoria)
                .Where(p => p.CategoriaId == categoriaId)
                .Select(p => new ProductoListDto()
                {
                    ProductoId = p.ProductoId,
                    NombreProducto = p.NombreProducto,
                    Categoria = p.Categoria.NombreCategoria,
                    PrecioUnitario = p.PrecioUnitario,
                    UnidadesDisponibles = p.Stock - p.UnidadesEnPedido,
                    Suspendido = p.Suspendido,
                    Imagen = p.Imagen
                }).ToList();

        }

        public int GetCantidad(Func<Producto, bool> predicado)
        {
            return _context.Productos.Count(predicado);
        }

        public int GetCantidad()
        {
            return _context.Productos.Count();
        }
    }
}
