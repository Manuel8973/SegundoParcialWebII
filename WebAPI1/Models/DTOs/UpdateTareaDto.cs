using System.ComponentModel.DataAnnotations;
using WebAPI1.Models.Enums;

namespace WebAPI1.Models.DTOs
{
    public class UpdateTareaDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Titulo { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Descripcion { get; set; } = string.Empty;

        [Required]
        public EstadoTarea Estado { get; set; }

        [Required]
        public int IdResponsable { get; set; }

        [Required]
        public int IdPrioridad { get; set; }
    }
}
