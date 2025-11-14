using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebAPI1.Models.Enums;

namespace WebAPI1.Models
{
    public class Prioridad
    {
        public int Id { get; set; }

        [Required]
        public NombrePrioridad Nombre { get; set; }

      
        [JsonIgnore] 
        public ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
    }
}
