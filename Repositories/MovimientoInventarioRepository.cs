using ProgramacioIV.Data;
using ProgramacioIV.DTOs.Movimientos;
using ProgramacioIV.Interfaces;

namespace ProgramacioIV.Repositories
{
    public class MovimientoInventarioRepository : IMovimientoInventarioRepository
    {
        private readonly AppDbContext _context;

        public MovimientoInventarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<MovimientoInventarioResponseDTO> ObtenerTodos()
        {
            return _context.MovimientosInventario
                .Join(
                    _context.Materiales,
                    movimiento => movimiento.MaterialId,
                    material => material.Id,
                    (movimiento, material) => new MovimientoInventarioResponseDTO
                    {
                        Id = movimiento.Id,
                        MaterialId = material.Id,
                        Material = material.Nombre,
                        TipoMovimiento = movimiento.TipoMovimiento,
                        Cantidad = movimiento.Cantidad,
                        StockAnterior = movimiento.StockAnterior,
                        StockNuevo = movimiento.StockNuevo,
                        Observacion = movimiento.Observacion,
                        FotoEvidencia = movimiento.FotoEvidencia,
                        RegistradoPorId = movimiento.RegistradoPorId,
                        FechaMovimiento = movimiento.FechaMovimiento
                    }
                )
                .OrderByDescending(m => m.FechaMovimiento)
                .ToList();
        }

        public MovimientoInventarioResponseDTO? ObtenerPorId(int id)
        {
            return _context.MovimientosInventario
                .Where(movimiento => movimiento.Id == id)
                .Join(
                    _context.Materiales,
                    movimiento => movimiento.MaterialId,
                    material => material.Id,
                    (movimiento, material) => new MovimientoInventarioResponseDTO
                    {
                        Id = movimiento.Id,
                        MaterialId = material.Id,
                        Material = material.Nombre,
                        TipoMovimiento = movimiento.TipoMovimiento,
                        Cantidad = movimiento.Cantidad,
                        StockAnterior = movimiento.StockAnterior,
                        StockNuevo = movimiento.StockNuevo,
                        Observacion = movimiento.Observacion,
                        FotoEvidencia = movimiento.FotoEvidencia,
                        RegistradoPorId = movimiento.RegistradoPorId,
                        FechaMovimiento = movimiento.FechaMovimiento
                    }
                )
                .FirstOrDefault();
        }

        public List<MovimientoInventarioResponseDTO> ObtenerPorMaterial(int materialId)
        {
            return _context.MovimientosInventario
                .Where(movimiento => movimiento.MaterialId == materialId)
                .Join(
                    _context.Materiales,
                    movimiento => movimiento.MaterialId,
                    material => material.Id,
                    (movimiento, material) => new MovimientoInventarioResponseDTO
                    {
                        Id = movimiento.Id,
                        MaterialId = material.Id,
                        Material = material.Nombre,
                        TipoMovimiento = movimiento.TipoMovimiento,
                        Cantidad = movimiento.Cantidad,
                        StockAnterior = movimiento.StockAnterior,
                        StockNuevo = movimiento.StockNuevo,
                        Observacion = movimiento.Observacion,
                        FotoEvidencia = movimiento.FotoEvidencia,
                        RegistradoPorId = movimiento.RegistradoPorId,
                        FechaMovimiento = movimiento.FechaMovimiento
                    }
                )
                .OrderByDescending(m => m.FechaMovimiento)
                .ToList();
        }
    }
}