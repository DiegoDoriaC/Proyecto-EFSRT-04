using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriasReto.DTO
{
    public class RecepcionDTO
    {
        public int IdRecepcion { get; set; }
        public int IdLibro { get; set; }
        public string? nombreLibro { get; set; }
        public int Cantidad { get; set; }
        public string? FechaIngreso { get; set; }
    }
}
