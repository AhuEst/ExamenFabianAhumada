using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamenFabianAhumada.Data
{
    public class Proveedor
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El Rut es requerido.")]
        [StringLength(10, ErrorMessage = "El rut no debe exceder los 10 caracteres.")]
        public string Rut { get; set; }

        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(50, ErrorMessage = "El nombre es demasiado largo.")]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]+$", ErrorMessage = "El nombre solo puede contener letras.")]
        public string Nombre { get; set; }


        [ForeignKey("Ubicacion")]
        [Required(ErrorMessage = "La ubicacion es requerida.")]
        public int UbicacionId { get; set; }
    }
}
