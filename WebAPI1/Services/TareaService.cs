using Microsoft.EntityFrameworkCore;
using WebAPI1.Data;
using WebAPI1.Models;
using WebAPI1.Models.DTOs;

namespace WebAPI1.Services
{
    public class TareaService
    {
        private readonly AppDbContext _context;

        public TareaService(AppDbContext context)
        {
            _context = context;
        }

        
        public async Task<List<Tarea>> ListarTareasAsync()
        {
            return await _context.Tareas
                .Include(t => t.Responsable)
                .Include(t => t.Prioridad)
                .ToListAsync();
        }

       
        public async Task<Tarea> CrearTareaAsync(CreateTareaDto dto)
        {
            
            var responsableExiste = await _context.Miembros.AnyAsync(m => m.Id == dto.IdResponsable);
            if (!responsableExiste)
            {
                throw new Exception($"El miembro con Id {dto.IdResponsable} no existe");
            }

            
            var prioridadExiste = await _context.Prioridades.AnyAsync(p => p.Id == dto.IdPrioridad);
            if (!prioridadExiste)
            {
                throw new Exception($"La prioridad con Id {dto.IdPrioridad} no existe");
            }

            var tarea = new Tarea
            {
                Titulo = dto.Titulo,
                Descripcion = dto.Descripcion,
                FechaCreacion = DateTime.Now,
                Estado = dto.Estado,
                IdResponsable = dto.IdResponsable,
                IdPrioridad = dto.IdPrioridad
            };

            _context.Tareas.Add(tarea);
            await _context.SaveChangesAsync();

            
            await _context.Entry(tarea).Reference(t => t.Responsable).LoadAsync();
            await _context.Entry(tarea).Reference(t => t.Prioridad).LoadAsync();

            return tarea;
        }

        
        public async Task<Tarea> EditarTareaAsync(UpdateTareaDto dto)
        {
            var tarea = await _context.Tareas.FindAsync(dto.Id);
            if (tarea == null)
            {
                throw new Exception($"La tarea con Id {dto.Id} no existe");
            }

            
            var responsableExiste = await _context.Miembros.AnyAsync(m => m.Id == dto.IdResponsable);
            if (!responsableExiste)
            {
                throw new Exception($"El miembro con Id {dto.IdResponsable} no existe");
            }

            
            var prioridadExiste = await _context.Prioridades.AnyAsync(p => p.Id == dto.IdPrioridad);
            if (!prioridadExiste)
            {
                throw new Exception($"La prioridad con Id {dto.IdPrioridad} no existe");
            }

            tarea.Titulo = dto.Titulo;
            tarea.Descripcion = dto.Descripcion;
            tarea.Estado = dto.Estado;
            tarea.IdResponsable = dto.IdResponsable;
            tarea.IdPrioridad = dto.IdPrioridad;

            await _context.SaveChangesAsync();

            
            await _context.Entry(tarea).Reference(t => t.Responsable).LoadAsync();
            await _context.Entry(tarea).Reference(t => t.Prioridad).LoadAsync();

            return tarea;
        }

        
        public async Task<Tarea> EliminarTareaAsync(int id)
        {
            var tarea = await _context.Tareas
                .Include(t => t.Responsable)
                .Include(t => t.Prioridad)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tarea == null)
            {
                throw new Exception($"La tarea con Id {id} no existe");
            }

            _context.Tareas.Remove(tarea);
            await _context.SaveChangesAsync();

            return tarea;
        }

       
        public async Task<List<Tarea>> OrdenarTareasPorPrioridadAsync()
        {
            return await _context.Tareas
                .Include(t => t.Responsable)
                .Include(t => t.Prioridad)
                .OrderByDescending(t => t.Prioridad!.Nombre) 
                .ThenBy(t => t.FechaCreacion)
                .ToListAsync();
        }

        
        public async Task<List<Tarea>> BuscarTareasPorTituloAsync(string titulo)
        {
            if (string.IsNullOrWhiteSpace(titulo))
            {
                return new List<Tarea>();
            }

            return await _context.Tareas
                .Include(t => t.Responsable)
                .Include(t => t.Prioridad)
                .Where(t => t.Titulo.Contains(titulo))
                .ToListAsync();
        }

        
        public async Task<List<Tarea>> ListarTareasPorMiembroAsync(int idMiembro)
        {
            
            var miembroExiste = await _context.Miembros.AnyAsync(m => m.Id == idMiembro);
            if (!miembroExiste)
            {
                throw new Exception($"El miembro con Id {idMiembro} no existe");
            }

            return await _context.Tareas
                .Include(t => t.Responsable)
                .Include(t => t.Prioridad)
                .Where(t => t.IdResponsable == idMiembro)
                .ToListAsync();
        }

        
        public async Task<Tarea?> ObtenerTareaPorIdAsync(int id)
        {
            return await _context.Tareas
                .Include(t => t.Responsable)
                .Include(t => t.Prioridad)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
