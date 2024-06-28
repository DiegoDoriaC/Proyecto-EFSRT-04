using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriasReto.DTO
{
    public class ComprobanteDTO
    {
        public int IdComprobante { get; set; }
        public int IdCliente { get; set; }
        public string? nombreCliente { get; set; }
        public int IdEmpleado { get; set; }
        public string? nombreEmpleado { get; set; }
        public int IdMetodoPago { get; set; }
        public string? nombreMetodoPago { get; set; }
        public decimal? Total { get; set; }
        public string? FechaVenta { get; set; }
        public virtual List<VentaDTO> Venta { get; set; } = new List<VentaDTO>();
    }
}
