using System;
using System.Collections.Generic;

namespace LibreriasReto.Models;

public partial class MetodoPago
{
    public int IdMetodoPago { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Comprobante> Comprobantes { get; set; } = new List<Comprobante>();
}
