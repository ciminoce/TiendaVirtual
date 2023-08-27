using System;

namespace TiendaVirtual.Entidades.Entidades
{
    public class Categoria
    {
        public int CategoriaId { get; set; }
        public string NombreCategoria { get; set; }
        public string Descripcion { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
