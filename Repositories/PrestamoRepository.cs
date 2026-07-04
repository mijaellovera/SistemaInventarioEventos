using Microsoft.EntityFrameworkCore;
using ProgramacioIV.Data;
using ProgramacioIV.DTOs.Prestamos;
using ProgramacioIV.Interfaces;
using ProgramacioIV.Models;

namespace ProgramacioIV.Repositories
{
    public class PrestamoRepository : IPrestamoRepository
    {
        private readonly AppDbContext _context;

        public PrestamoRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Prestamo> ObtenerTodos()
        {
            return _context.Prestamos.ToList();
        }

        public Prestamo? ObtenerPorId(int id)
        {
            return _context.Prestamos.Find(id);
        }

        public Prestamo Crear(Prestamo prestamo)
        {
            _context.Prestamos.Add(prestamo);
            _context.SaveChanges();

            return prestamo;
        }

        public Prestamo? CambiarEstado(int id, string estado)
        {
            var prestamo = _context.Prestamos.Find(id);

            if (prestamo == null)
            {
                return null;
            }

            prestamo.Estado = estado;

            _context.SaveChanges();

            return prestamo;
        }
    

    public PrestamoHistorialDTO? ObtenerHistorial(int id)
        {
            var prestamo = _context.Prestamos.Find(id);

            if (prestamo == null)
            {
                return null;
            }

            var materiales = _context.DetallePrestamos
                .Where(d => d.PrestamoId == id)
                .Join(
                    _context.Materiales,
                    detalle => detalle.MaterialId,
                    material => material.Id,
                    (detalle, material) => new DetallePrestamoHistorialDTO
                    {
                        MaterialId = material.Id,
                        Material = material.Nombre,
                        CantidadPrestada = detalle.CantidadPrestada,
                        CantidadDevuelta = detalle.CantidadDevuelta,
                        CantidadPendiente = detalle.CantidadPrestada - detalle.CantidadDevuelta,
                        EstadoDevolucion = detalle.EstadoDevolucion
                    }
                )
                .ToList();

            return new PrestamoHistorialDTO
            {
                Id = prestamo.Id,
                CodigoPrestamo = prestamo.CodigoPrestamo,
                Solicitante = prestamo.Solicitante,
                FechaPrestamo = prestamo.FechaPrestamo,
                Estado = prestamo.Estado,
                FechaRegistro = prestamo.FechaRegistro,
                Materiales = materiales
            };
        }
    }

}