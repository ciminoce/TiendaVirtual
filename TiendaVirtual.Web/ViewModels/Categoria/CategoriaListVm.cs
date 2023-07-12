using System.ComponentModel;

namespace TiendaVirtual.Web.ViewModels.Categoria
{
    public class CategoriaListVm
    {
        public int CategoriaId { get; set; }
        [DisplayName("Categoría")]
        public string NombreCategoria { get; set; }
        [DisplayName("Cant. Productos")]
        public int CantidadProductos { get; set; }
    }
}