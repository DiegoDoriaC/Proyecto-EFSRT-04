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
        [Required(ErrorMessage = "La area es requerida")]
        public int? IdArea { get; set; }
        public string? nombreArea { get; set; }
        [Required(ErrorMessage = "El dni es requerido")]
        [RegularExpression("^[0-9]{8}$", ErrorMessage = "El DNI debe contener 8 digitos")]
        public string Dni { get; set; } = null!;
        [Required(ErrorMessage = "El nombre es requerido")]
        [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$", ErrorMessage = "Solo se aceptan letras")]
        [MaxLength(30, ErrorMessage = "Maximo 30 caracteres")]
        public string Nombre { get; set; } = null!;
        [Required(ErrorMessage = "El apellido es requerido")]
        [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$", ErrorMessage = "Solo se aceptan letras")]
        [MaxLength(30, ErrorMessage = "Maximo 30 caracteres")]
        public string Apellido { get; set; } = null!;
        [Required(ErrorMessage = "El apellido es requerido")]
        [RegularExpression("^[0-9]{9}$", ErrorMessage = "El telefono debe contener 9 digitos")]
        public string Telefono { get; set; } = null!;
        [Required(ErrorMessage = "La fecha es requerida")]
        public string? FechaIngreso { get; set; }
        [Required(ErrorMessage = "El correo es requerido")]
        [EmailAddress(ErrorMessage = "Correo invalido")]
        [MaxLength(45, ErrorMessage = "Maximo 45 caracteres")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "La direccion es requerida")]
        [MaxLength(40, ErrorMessage = "Maximo 40 caracteres")]
        public string? Direccion { get; set; }
        public bool? EsActivo { get; set; }
        public virtual AreaDTO? IdAreaNavigation { get; set; }
    }
}
