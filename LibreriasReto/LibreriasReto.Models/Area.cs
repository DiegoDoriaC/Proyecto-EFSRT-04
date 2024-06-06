using System;
using System.Collections.Generic;

namespace LibreriasReto.Models;

public partial class Area
{
    public int IdArea { get; set; }
    public string Cargo { get; set; } = null!;
    public decimal Sueldo { get; set; }
    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
