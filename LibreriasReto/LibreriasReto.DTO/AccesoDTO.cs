using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriasReto.DTO
{
    public class AccesoDTO
    {
        public int IdAcceso { get; set; }
        public int IdEmpleado { get; set; }
        public string? dniEmpleado { get; set; }
        public string? EmpleadoNombre { get; set; }
        public string? EmpleadoRol { get; set; }
        public string Clave { get; set; } = null!;
    }
}
