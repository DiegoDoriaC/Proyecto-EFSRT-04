using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriasReto.DTO
{
    public class EmpleadoDTO
    {
        public int IdEmpleado { get; set; }
        public int? IdArea { get; set; }
        public string? nombreArea { get; set; }
        [MaxLength(8)]
        public string Dni { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string? FechaIngreso { get; set; }
        public string? Email { get; set; }
        public string? Direccion { get; set; }
        public bool? EsActivo { get; set; }
        public virtual AreaDTO? IdAreaNavigation { get; set; }
    }
}
