using System.ComponentModel.DataAnnotations;
using WebAPI1.Models.Enums;

namespace WebAPI1.Models.DTOs
{
    public class UpdatePrioridadDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public NombrePrioridad Nombre { get; set; }
    }
}
