using Microsoft.EntityFrameworkCore;
using WebAPI1.Data;
using WebAPI1.Models;
using WebAPI1.Models.DTOs;

namespace WebAPI1.Services
{
    public class PrioridadService
    {
        private readonly AppDbContext _context;

        public PrioridadService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<List<Prioridad>> ListarPrioridadesAsync()
        {
            return await _context.Prioridades.ToListAsync();
        }

        
        public async Task<Prioridad> CrearPrioridadAsync(CreatePrioridadDto dto)
        {
            
            var prioridadExiste = await _context.Prioridades
                .AnyAsync(p => p.Nombre == dto.Nombre);

            if (prioridadExiste)
            {
                throw new Exception($"Ya existe una prioridad con el nombre '{dto.Nombre}'");
            }

            var prioridad = new Prioridad
            {
                Nombre = dto.Nombre
            };

            _context.Prioridades.Add(prioridad);
            await _context.SaveChangesAsync();

            return prioridad;
        }

        
        public async Task<Prioridad> EditarPrioridadAsync(UpdatePrioridadDto dto)
        {
            var prioridad = await _context.Prioridades.FindAsync(dto.Id);
            if (prioridad == null)
            {
                throw new Exception($"La prioridad con Id {dto.Id} no existe");
            }

            
            var nombreDuplicado = await _context.Prioridades
                .AnyAsync(p => p.Nombre == dto.Nombre && p.Id != dto.Id);

            if (nombreDuplicado)
            {
                throw new Exception($"Ya existe otra prioridad con el nombre '{dto.Nombre}'");
            }

            prioridad.Nombre = dto.Nombre;

            await _context.SaveChangesAsync();

            return prioridad;
        }

     
        public async Task<Prioridad> EliminarPrioridadAsync(int id)
        {
            var prioridad = await _context.Prioridades.FindAsync(id);
            if (prioridad == null)
            {
                throw new Exception($"La prioridad con Id {id} no existe");
            }

            
            var tieneTareas = await _context.Tareas.AnyAsync(t => t.IdPrioridad == id);
            if (tieneTareas)
            {
                throw new Exception($"No se puede eliminar la prioridad porque tiene tareas asociadas");
            }

            _context.Prioridades.Remove(prioridad);
            await _context.SaveChangesAsync();

            return prioridad;
        }

        
        public async Task<Prioridad?> ObtenerPrioridadPorIdAsync(int id)
        {
            return await _context.Prioridades.FindAsync(id);
        }
    }
}
