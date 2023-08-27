using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace TiendaVirtual.Web.ViewModels.Producto
{
    public class ProductoEditVm
    {
        public int ProductoId { get; set; }
        [DisplayName("Producto")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(100, ErrorMessage = "El campo {0} debe contener entre {2} y {1} caracteres", MinimumLength = 3)]
        public string NombreProducto { get; set; }
        [DisplayName("Proveedor")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un proveedor")]

        public int ProveedorId { get; set; }
        [DisplayName("Categoría")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una categoría")]

        public int CategoriaId { get; set; }

        [DisplayName("Precio Unit.")]
        [Required(ErrorMessage = "El precio es requerido")]
        [Range(0.10, 10000, ErrorMessage = "Favor de ingresar un {0} entre {1} y{2}")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}")]
        
        
        public decimal PrecioUnitario { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Stock mal ingresado")]

        public int Stock { get; set; }
        [DisplayName("Stock Mínimo")]
        [Range(1, int.MaxValue, ErrorMessage = "Stock mínimo mal ingresado")]

        public int StockMinimo { get; set; } = 0;
        public bool Suspendido { get; set; } = false;
        [DataType(DataType.ImageUrl)]
        public string Imagen { get; set; }
        [DisplayName("Imagen")]
        public HttpPostedFileBase imagenFile { get; set; }
        public byte[] RowVersion { get; set; }
        public List<SelectListItem> Proveedores { get; set; }
        public List<SelectListItem> Categorias { get; set; }



    }
}