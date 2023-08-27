using TiendaVirtual.Entidades.Entidades;

namespace TiendaVirtual.Servicios.Interfaces
{
    public interface IGateway
    {
        PaymentResult ProcesarPago(CheckOut model);
    }
}
