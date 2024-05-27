using System;
using System.Collections.Generic;

namespace LibreriasReto.Models;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public int? IdArea { get; set; }

    public string Dni { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public DateOnly FechaIngreso { get; set; }

    public string? Email { get; set; }

    public string? Direccion { get; set; }

    public bool? EsActivo { get; set; }

    public virtual ICollection<Acceso> Accesos { get; set; } = new List<Acceso>();

    public virtual ICollection<Comprobante> Comprobantes { get; set; } = new List<Comprobante>();

    public virtual Area? IdAreaNavigation { get; set; }
}
