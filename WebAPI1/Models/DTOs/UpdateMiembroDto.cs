using System.ComponentModel.DataAnnotations;
using WebAPI1.Models.Enums;

namespace WebAPI1.Models.DTOs
{
    public class UpdateMiembroDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombres { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Apellidos { get; set; } = string.Empty;

        [Required]
        public RolMiembro Rol { get; set; }
    }
}
