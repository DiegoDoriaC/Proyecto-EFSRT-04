using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriasReto.DTO
{
    public class AreaDTO
    {
        public int IdArea { get; set; }
        public string Cargo { get; set; } = null!;
        public decimal Sueldo { get; set; }
        public virtual ICollection<EmpleadoDTO> Empleados { get; set; } = new List<EmpleadoDTO>();
    }
}
