using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriasReto.DTO
{
    public class LibroDTO
    {
        public int IdLibro { get; set; }
        [Required(ErrorMessage = "El genero es requerido")]
        public int? IdGenero { get; set; }
        public string? nombreGenero { get; set; }
        [Required(ErrorMessage = "El nombre del libro es requerido")]
        [MaxLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string Nombre { get; set; } = null!;
        [Required(ErrorMessage = "El autor es requerido")]
        [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$", ErrorMessage = "Solo se aceptan letras")]
        [MaxLength(25, ErrorMessage = "Maximo 25 caracteres")]
        public string Autor { get; set; } = null!;
        [Required(ErrorMessage = "La editorial requerida")]
        [MaxLength(20, ErrorMessage = "Maximo 20 caracteres")]
        public string Editorial { get; set; } = null!;
        [Required(ErrorMessage = "El precio es requerido")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "El precio debe ser un número con hasta 2 decimales")]
        [MaxLength(10, ErrorMessage = "El precio es demasiado largo")]
        public string? Precio { get; set; }
        [Required(ErrorMessage = "El año es requerido")]
        [RegularExpression("^(19|20)[0-9]{2}$", ErrorMessage = "Introduce un año válido entre 1900 y 2099")]
        public string? AnioPublicacion { get; set; }
        public int? Stock { get; set; }
        [Required(ErrorMessage = "La url de requerida")]
        [StringLength(550, ErrorMessage = "La url debe tener maximo 550 caracteres")]
        public string? urlImagen { get; set; }
        public bool? EsActivo { get; set; }
    }
}
