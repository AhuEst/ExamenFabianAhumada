using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ExamenFabianAhumada.Data
{
    public class Ubicacion
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(50, ErrorMessage = "El nombre es demasiado largo.")]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]+$", ErrorMessage = "El nombre solo puede contener letras.")]
        public string Nombre { get; set; }

        public async Task<int> ContarProveedoresAsync(EjemploDbContext context)
        {
            return await context.Proveedor.CountAsync(p => p.UbicacionId == this.Id);
        }

    }
}
