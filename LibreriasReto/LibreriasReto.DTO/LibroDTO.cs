using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriasReto.DTO
{
    public class LibroDTO
    {
        public int IdLibro { get; set; }
        public int? IdGenero { get; set; }
        public string? nombreGenero { get; set; }
        public string Nombre { get; set; } = null!;
        public string Autor { get; set; } = null!;
        public string Editorial { get; set; } = null!;
        public string? Precio { get; set; }
        public string? AnioPublicacion { get; set; }
        public int? Stock { get; set; }
        public bool? EsActivo { get; set; }
    }
}
