using System;
using System.Collections.Generic;

namespace LibreriasReto.Models;

public partial class Recepcion
{
    public int IdRecepcion { get; set; }

    public int IdLibro { get; set; }

    public int Cantidad { get; set; }

    public DateOnly? FechaIngreso { get; set; }

    public virtual Libro IdLibroNavigation { get; set; } = null!;
}
