using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebAPI1.Models.Enums;

namespace WebAPI1.Models
{
    public class Miembro
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombres { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Apellidos { get; set; } = string.Empty;

        [Required]
        public RolMiembro Rol { get; set; }

     
        [JsonIgnore] 
        public ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
    }
}
