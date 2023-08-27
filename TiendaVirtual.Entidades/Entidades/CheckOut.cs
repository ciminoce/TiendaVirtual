namespace TiendaVirtual.Entidades.Entidades
{
    public class CheckOut
    {
        public Venta Venta { get; set; }
        public string CardNumber { get; set; }

        public string CVV { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
    }
}
