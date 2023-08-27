namespace TiendaVirtual.Entidades.Entidades
{
    public abstract class Persona
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public int PaisId { get; set; }
        public int CiudadId { get; set; }
        public string CodPostal { get; set; }
        public byte[] RowVersion { get; set; }

        public Pais Pais { get; set; }
        public Ciudad Ciudad { get; set; }

    }
}
