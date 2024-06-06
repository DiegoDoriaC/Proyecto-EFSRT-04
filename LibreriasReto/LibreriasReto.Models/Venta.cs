using System;
using System.Collections.Generic;

namespace LibreriasReto.Models;

public partial class Venta
{
    public int IdVenta { get; set; }
    public int? IdComprobante { get; set; }
    public int Idlibro { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
    public decimal? Total { get; set; }
    public virtual Comprobante? IdComprobanteNavigation { get; set; }
    public virtual Libro IdlibroNavigation { get; set; } = null!;
}
