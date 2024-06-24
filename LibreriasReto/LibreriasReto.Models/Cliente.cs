using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibreriasReto.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }
    public string? Dni { get; set; }
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public bool? EsActivo { get; set; }
    public virtual ICollection<Comprobante> Comprobantes { get; set; } = new List<Comprobante>();
}
