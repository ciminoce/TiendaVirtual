using TiendaVirtual.Entidades.Dtos.Ciudad;

namespace TiendaVirtual.Entidades.Entidades
{
    public class Ciudad
    {
        public int CiudadId { get; set; }
        public string NombreCiudad { get; set; }
        public int PaisId { get; set; }
        public byte[] RowVersion { get; set; }
        public Pais Pais { get; set; }
        public CiudadListDto ToCiudadListDto()
        {
            return new CiudadListDto()
            {
                CiudadId = CiudadId,
                NombreCiudad = NombreCiudad,
                NombrePais = Pais.NombrePais
            };
        }
    }
}
