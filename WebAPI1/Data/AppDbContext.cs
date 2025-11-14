using Microsoft.EntityFrameworkCore;
using WebAPI1.Models;

namespace WebAPI1.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Tarea> Tareas => Set<Tarea>();
        public DbSet<Miembro> Miembros => Set<Miembro>();
        public DbSet<Prioridad> Prioridades => Set<Prioridad>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Tarea>()
                .HasOne(t => t.Responsable)
                .WithMany(m => m.Tareas)
                .HasForeignKey(t => t.IdResponsable)
                .OnDelete(DeleteBehavior.Restrict);

            
            modelBuilder.Entity<Tarea>()
                .HasOne(t => t.Prioridad)
                .WithMany(p => p.Tareas)
                .HasForeignKey(t => t.IdPrioridad)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
