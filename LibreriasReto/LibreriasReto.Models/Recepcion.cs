using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibreriasReto.Models;

public partial class Recepcion
{
    public int IdRecepcion { get; set; }

    public int IdLibro { get; set; }

    public int Cantidad { get; set; }

    public DateTime? FechaIngreso { get; set; }

    public virtual Libro IdLibroNavigation { get; set; } = null!;
}
