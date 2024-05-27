using System;
using System.Collections.Generic;

namespace LibreriasReto.Models;

public partial class Acceso
{
    public int IdAcceso { get; set; }

    public int IdEmpleado { get; set; }

    public string Clave { get; set; } = null!;

    public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;
}
