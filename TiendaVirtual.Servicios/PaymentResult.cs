namespace TiendaVirtual.Servicios
{
    public class PaymentResult
    {
        public PaymentResult(string transaccionId, bool exitoso, string mensaje)
        {
            TransaccionId = transaccionId;
            Exitoso = exitoso;
            Mensaje = mensaje;
        }

        public string TransaccionId { get; private set; }
        public bool Exitoso { get; private set; }
        public string Mensaje { get; private set; }
        
    }
}