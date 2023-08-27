using System;

namespace TiendaVirtual.Entidades.Entidades
{
    public class CtaCte
    {
        public int CtaCteId { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public string Movimiento { get; set; }
        public decimal Debe { get; set; }
        public decimal Haber { get; set; }
        public decimal Saldo { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }


    }
}
