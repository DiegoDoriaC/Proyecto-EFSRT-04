using System;
using System.Collections.Generic;

namespace LibreriasReto.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string Dni { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public bool? EsActivo { get; set; }

    public virtual ICollection<Comprobante> Comprobantes { get; set; } = new List<Comprobante>();
}
