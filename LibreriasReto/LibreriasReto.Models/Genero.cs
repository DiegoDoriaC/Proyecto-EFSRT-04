﻿using System;
using System.Collections.Generic;

namespace LibreriasReto.Models;

public partial class Genero
{
    public int IdGenero { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
