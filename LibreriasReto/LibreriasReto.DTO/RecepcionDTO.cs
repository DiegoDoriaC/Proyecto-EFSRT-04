using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriasReto.DTO
{
    public class RecepcionDTO
    {
        public int IdRecepcion { get; set; }
        [Required(ErrorMessage = "El libro es requerido")]
        public int IdLibro { get; set; }
        public string? nombreLibro { get; set; }
        [Required(ErrorMessage = "La cantidad es requerida")]
        [Range(1, 999, ErrorMessage = "La cantidad de ser entre 1 y 999")]
        public int Cantidad { get; set; }
        [Required(ErrorMessage = "La fecha es requerida")]
        public string? FechaIngreso { get; set; }
    }
}
