using System.ComponentModel.DataAnnotations;

namespace ExamenFabianAhumada.Data
{
    public class TipoProducto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(50, ErrorMessage = "El nombre es demasiado largo.")]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]+$", ErrorMessage = "El nombre solo puede contener letras.")]
        public string Nombre { get; set; }

        [StringLength(200, ErrorMessage = "La descripcion es demasiado larga.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El estado es requerido.")]
        [StringLength(25)]
        public string Estado { get; set; }
    }
}
