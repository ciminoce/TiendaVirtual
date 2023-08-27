using TiendaVirtual.Entidades.Dtos.Cliente;

namespace TiendaVirtual.Entidades.Entidades
{
    public class Cliente : Persona
    {
        public string TelefonoFijo { get; set; }
        public string TelefonoMovil { get; set; }
        public string Email { get; set; }

        public ClienteListDto ToClienteListDto()
        {
            return new ClienteListDto
            {
                ClienteId = Id,
                NombreCliente = Nombre,
                Pais = Pais.NombrePais,
                Ciudad = Ciudad.NombreCiudad
            };
        }
    }
}
