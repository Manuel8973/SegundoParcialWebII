using Microsoft.EntityFrameworkCore;
using WebAPI1.Data;
using WebAPI1.Models;
using WebAPI1.Models.DTOs;

namespace WebAPI1.Services
{
    public class MiembroService
    {
        private readonly AppDbContext _context;

        public MiembroService(AppDbContext context)
        {
            _context = context;
        }

        
        public async Task<List<Miembro>> ListarMiembrosAsync()
        {
            return await _context.Miembros.ToListAsync();
        }

        
        public async Task<Miembro> CrearMiembroAsync(CreateMiembroDto dto)
        {
            var miembro = new Miembro
            {
                Nombres = dto.Nombres,
                Apellidos = dto.Apellidos,
                Rol = dto.Rol
            };

            _context.Miembros.Add(miembro);
            await _context.SaveChangesAsync();

            return miembro;
        }

        
        public async Task<Miembro> EditarMiembroAsync(UpdateMiembroDto dto)
        {
            var miembro = await _context.Miembros.FindAsync(dto.Id);
            if (miembro == null)
            {
                throw new Exception($"El miembro con Id {dto.Id} no existe");
            }

            miembro.Nombres = dto.Nombres;
            miembro.Apellidos = dto.Apellidos;
            miembro.Rol = dto.Rol;

            await _context.SaveChangesAsync();

            return miembro;
        }

        public async Task<Miembro> EliminarMiembroAsync(int id)
        {
            var miembro = await _context.Miembros.FindAsync(id);
            if (miembro == null)
            {
                throw new Exception($"El miembro con Id {id} no existe");
            }

            
            var tieneTareas = await _context.Tareas.AnyAsync(t => t.IdResponsable == id);
            if (tieneTareas)
            {
                throw new Exception($"No se puede eliminar el miembro porque tiene tareas asignadas");
            }

            _context.Miembros.Remove(miembro);
            await _context.SaveChangesAsync();

            return miembro;
        }

        
        public async Task<Miembro?> ObtenerMiembroPorIdAsync(int id)
        {
            return await _context.Miembros.FindAsync(id);
        }
    }
}
