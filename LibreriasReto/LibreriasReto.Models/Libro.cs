using System;
using System.Collections.Generic;

namespace LibreriasReto.Models;

public partial class Libro
{
    public int IdLibro { get; set; }

    public int? IdGenero { get; set; }

    public string Nombre { get; set; } = null!;

    public string Autor { get; set; } = null!;

    public string Editorial { get; set; } = null!;

    public int Precio { get; set; }

    public int AnioPublicacion { get; set; }

    public int? Stock { get; set; }

    public bool? EsActivo { get; set; }

    public virtual Genero? IdGeneroNavigation { get; set; }

    public virtual ICollection<Recepcion> Recepcions { get; set; } = new List<Recepcion>();

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
