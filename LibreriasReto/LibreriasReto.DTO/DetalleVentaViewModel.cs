using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriasReto.DTO
{
    public class DetalleVentaViewModel
    {
        public ComprobanteDTO Comprobante { get; set; }
        public List<VentaDTO> Venta { get; set; }
    }
}
