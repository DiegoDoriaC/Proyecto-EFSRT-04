using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriasReto.DTO
{
    public class VentaDTO
    {
        public int IdVenta { get; set; }
        public int? IdComprobante { get; set; }
        public int Idlibro { get; set; }
        public string? nombreLibro { get; set; }
        public int Cantidad { get; set; }
        public decimal? Precio { get; set; }
        public decimal? Total { get; set; }
        public virtual ComprobanteDTO? IdComprobanteNavigation { get; set; }
    }
}
