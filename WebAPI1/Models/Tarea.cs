using System.ComponentModel.DataAnnotations;
using WebAPI1.Models.Enums;

namespace WebAPI1.Models
{
    public class Tarea
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Titulo { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Descripcion { get; set; } = string.Empty;

        [Required]
        public DateTime FechaCreacion { get; set; }

        [Required]
        public EstadoTarea Estado { get; set; }

   
        [Required]
        public int IdResponsable { get; set; }

        
        public Miembro? Responsable { get; set; }

        
        [Required]
        public int IdPrioridad { get; set; }

        
        public Prioridad? Prioridad { get; set; }
    }
}
