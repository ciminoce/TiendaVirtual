using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TiendaVirtual.Web.ViewModels.Categoria
{
    public class CategoriaEditVm
    {
        public int CategoriaId { get; set; }

        [DisplayName("Categoría")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(100, ErrorMessage = "El campo {0} debe contener entre {2} y {1} caracteres", MinimumLength = 3)]
        public string NombreCategoria { get; set; }

        [DisplayName("Descripción")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(255, ErrorMessage = "El campo {0} no debe contenermás de {1} caracteres")]

        public string Descripcion { get; set; }
        public byte[] RowVersion { get; set; }


    }
}