using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriasReto.DTO
{
    public class MetodoPagoDTO
    {
        public int IdMetodoPago { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$", ErrorMessage = "Solo se aceptan letras")]
        [MaxLength(25, ErrorMessage = "Maximo 25 caracteres")]
        public string? Nombre { get; set; }
    }
}
