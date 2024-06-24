using System.ComponentModel.DataAnnotations;

namespace LibreriasReto.DTO
{
    public class AreaDTO
    {
        public int IdArea { get; set; }
        [Required(ErrorMessage = "El cargo es requerido")]
        [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$", ErrorMessage = "Solo se aceptan letras")]
        [MaxLength(45, ErrorMessage = "Maxino 45 caracteres")]
        public string Cargo { get; set; } = null!;
        [Required(ErrorMessage = "El sueldo es requerido")]
        [Range(0, 999999, ErrorMessage = "El sueldo debe estar entre 0 y 999,999")]
        public decimal Sueldo { get; set; }
        public virtual ICollection<EmpleadoDTO> Empleados { get; set; } = new List<EmpleadoDTO>();
    }
}
