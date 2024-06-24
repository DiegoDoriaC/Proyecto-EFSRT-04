using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriasReto.DTO
{
    public class ClienteDTO
    {
        public int IdCliente { get; set; }
        [Required(ErrorMessage = "El DNI es requerido")]
        [RegularExpression("^[0-9]{8}$", ErrorMessage = "El DNI debe contener exactamente 8 números")]
        public string Dni { get; set; } = null!;

        [Required(ErrorMessage = "El nombre es requerido")]
        [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$", ErrorMessage = "Solo se aceptan letras")]
        [MaxLength(30, ErrorMessage = "Máximo 30 caracteres")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El apellido es requerido")]
        [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$", ErrorMessage = "Solo se aceptan letras")]
        [MaxLength(30, ErrorMessage = "Máximo 30 caracteres")]
        public string Apellido { get; set; } = null!;
        public bool? EsActivo { get; set; }

    }
}
