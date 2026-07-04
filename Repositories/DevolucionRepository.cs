using ProgramacioIV.Data;
using ProgramacioIV.DTOs.Devoluciones;
using ProgramacioIV.Interfaces;
using ProgramacioIV.Models;

namespace ProgramacioIV.Repositories
{
    public class DevolucionRepository : IDevolucionRepository
    {
        private readonly AppDbContext _context;

        public DevolucionRepository(AppDbContext context)
        {
            _context = context;
        }

        public DevolucionResponseDTO RegistrarDevolucion(DevolucionCreateDTO devolucionDTO)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var prestamo = _context.Prestamos.Find(devolucionDTO.PrestamoId);

                if (prestamo == null)
                    throw new Exception("Préstamo no encontrado.");

                var material = _context.Materiales.Find(devolucionDTO.MaterialId);

                if (material == null)
                    throw new Exception("Material no encontrado.");

                var detalle = _context.DetallePrestamos.FirstOrDefault(d =>
                    d.PrestamoId == devolucionDTO.PrestamoId &&
                    d.MaterialId == devolucionDTO.MaterialId &&
                    d.EstadoDevolucion != "Devuelto"
                );

                if (detalle == null)
                    throw new Exception("No existe una devolución pendiente para este material.");

                if (devolucionDTO.CantidadDevuelta <= 0)
                    throw new Exception("La cantidad devuelta debe ser mayor a cero.");

                int cantidadPendiente = detalle.CantidadPrestada - detalle.CantidadDevuelta;

                if (devolucionDTO.CantidadDevuelta > cantidadPendiente)
                    throw new Exception("La cantidad devuelta no puede ser mayor a la cantidad pendiente.");

                int stockAnterior = material.StockActual;
                material.StockActual += devolucionDTO.CantidadDevuelta;

                detalle.CantidadDevuelta += devolucionDTO.CantidadDevuelta;

                if (detalle.CantidadDevuelta == detalle.CantidadPrestada)
                {
                    detalle.EstadoDevolucion = "Devuelto";
                    detalle.FechaDevolucionReal = DateTime.Now;
                }
                else
                {
                    detalle.EstadoDevolucion = "Parcial";
                }
                detalle.ObservacionDevolucion = devolucionDTO.Observacion;
                detalle.FotoEvidenciaDevolucion = devolucionDTO.FotoEvidencia;
                detalle.DevueltoPorId = devolucionDTO.RegistradoPorId;

                var movimiento = new MovimientoInventario
                {
                    MaterialId = material.Id,
                    TipoMovimiento = "Devolucion",
                    Cantidad = devolucionDTO.CantidadDevuelta,
                    StockAnterior = stockAnterior,
                    StockNuevo = material.StockActual,
                    Observacion = devolucionDTO.Observacion,
                    FotoEvidencia = devolucionDTO.FotoEvidencia,
                    RegistradoPorId = devolucionDTO.RegistradoPorId,
                    FechaMovimiento = DateTime.Now
                };

                _context.MovimientosInventario.Add(movimiento);

                var detallesPrestamo = _context.DetallePrestamos
                     .Where(d => d.PrestamoId == devolucionDTO.PrestamoId)
                     .ToList();

                bool todosDevueltos = detallesPrestamo.All(d => d.EstadoDevolucion == "Devuelto");
                bool algunoDevuelto = detallesPrestamo.Any(d => d.CantidadDevuelta > 0);

                if (todosDevueltos)
                {
                    prestamo.Estado = "Finalizado";
                }
                else if (algunoDevuelto)
                {
                    prestamo.Estado = "Parcial";
                }

                _context.SaveChanges();
                transaction.Commit();

                return new DevolucionResponseDTO
                {
                    PrestamoId = prestamo.Id,
                    MaterialId = material.Id,
                    Material = material.Nombre,
                    CantidadDevuelta = devolucionDTO.CantidadDevuelta,
                    StockActual = material.StockActual,
                    EstadoPrestamo = prestamo.Estado,
                    FechaDevolucion = DateTime.Now
                };
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}