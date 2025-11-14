using System.ComponentModel.DataAnnotations;
using WebAPI1.Models.Enums;

namespace WebAPI1.Models.DTOs
{
    public class CreatePrioridadDto
    {
        [Required]
        public NombrePrioridad Nombre { get; set; }
    }
}
