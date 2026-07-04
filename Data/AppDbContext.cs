using Microsoft.EntityFrameworkCore;
using ProgramacioIV.Models;

namespace ProgramacioIV.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Material> Materiales { get; set; }

        public DbSet<Prestamo> Prestamos { get; set; }

        public DbSet<DetallePrestamo> DetallePrestamos { get; set; }

        public DbSet<MovimientoInventario> MovimientosInventario { get; set; }

        public DbSet<Auditoria> Auditorias { get; set; }
    }
}