using ProgramacioIV.Data;
using ProgramacioIV.DTOs.Prestamos;
using ProgramacioIV.Interfaces;
using ProgramacioIV.Models;

namespace ProgramacioIV.Services
{
    public class PrestamoService : IPrestamoService
    {
        private readonly IPrestamoRepository _prestamoRepository;
        private readonly AppDbContext _context;

        public PrestamoService(IPrestamoRepository prestamoRepository, AppDbContext context)
        {
            _prestamoRepository = prestamoRepository;
            _context = context;
        }

        public List<PrestamoResponseDTO> ObtenerTodos()
        {
            var prestamos = _prestamoRepository.ObtenerTodos();
            var detalles = _context.DetallePrestamos.ToList();

            return prestamos.Select(p => new PrestamoResponseDTO
            {
                Id = p.Id,
                CodigoPrestamo = p.CodigoPrestamo,
                Solicitante = p.Solicitante,
                FechaPrestamo = p.FechaPrestamo,
                Estado = p.Estado,
                RegistradoPorId = p.RegistradoPorId,
                FechaRegistro = p.FechaRegistro,

                Detalles = detalles
         .Where(d => d.PrestamoId == p.Id)
         .Select(d => new DetallePrestamoResponseDTO
         {
             Id = d.Id,
             MaterialId = d.MaterialId,
             MaterialNombre = _context.Materiales
                 .Where(m => m.Id == d.MaterialId)
                 .Select(m => m.Nombre)
                 .FirstOrDefault(),
             CantidadPrestada = d.CantidadPrestada,
             CantidadDevuelta = d.CantidadDevuelta,
             EstadoDevolucion = d.EstadoDevolucion,
             FotoEvidenciaDevolucion = d.FotoEvidenciaDevolucion,
             ObservacionDevolucion = d.ObservacionDevolucion,
             FechaDevolucionReal = d.FechaDevolucionReal,
             DevueltoPorId = d.DevueltoPorId,
             DevueltoPorNombre = _context.Usuarios
                 .Where(u => u.Id == d.DevueltoPorId)
                 .Select(u => u.NombreCompleto)
                 .FirstOrDefault()
         })
         .ToList()
            }).ToList();
        }

        public PrestamoResponseDTO? ObtenerPorId(int id)
        {
            var prestamo = _prestamoRepository.ObtenerPorId(id);

            if (prestamo == null)
            {
                return null;
            }

            var detalles = _context.DetallePrestamos
    .Where(d => d.PrestamoId == prestamo.Id)
    .Select(d => new DetallePrestamoResponseDTO
    {
        Id = d.Id,
        MaterialId = d.MaterialId,

        MaterialNombre = _context.Materiales
            .Where(m => m.Id == d.MaterialId)
            .Select(m => m.Nombre)
            .FirstOrDefault(),

        CantidadPrestada = d.CantidadPrestada,
        CantidadDevuelta = d.CantidadDevuelta,
        EstadoDevolucion = d.EstadoDevolucion,

        DevueltoPorNombre = _context.Usuarios
            .Where(u => u.Id == d.DevueltoPorId)
            .Select(u => u.NombreCompleto)
            .FirstOrDefault(),

        FotoEvidenciaDevolucion = d.FotoEvidenciaDevolucion,
        ObservacionDevolucion = d.ObservacionDevolucion,
        FechaDevolucionReal = d.FechaDevolucionReal,
        DevueltoPorId = d.DevueltoPorId
    })
    .ToList();

            return new PrestamoResponseDTO
            {
                Id = prestamo.Id,
                CodigoPrestamo = prestamo.CodigoPrestamo,
                Solicitante = prestamo.Solicitante,
               
                FechaPrestamo = prestamo.FechaPrestamo,
                Estado = prestamo.Estado,
                RegistradoPorId = prestamo.RegistradoPorId,
                FechaRegistro = prestamo.FechaRegistro,
                Detalles = detalles
            };
        }

        public PrestamoResponseDTO Crear(PrestamoCreateDTO prestamoDTO)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var ultimoPrestamo = _prestamoRepository.ObtenerTodos()
                    .OrderByDescending(p => p.Id)
                    .FirstOrDefault();

                int siguienteNumero = ultimoPrestamo == null ? 1 : ultimoPrestamo.Id + 1;

                string codigoPrestamo = $"PRE-{siguienteNumero:D6}";
                var prestamo = new Prestamo
                {
                    CodigoPrestamo = codigoPrestamo,
                    Solicitante = prestamoDTO.Solicitante,

                    FechaPrestamo = prestamoDTO.FechaPrestamo,
                    Estado = "Pendiente",
                    RegistradoPorId = prestamoDTO.RegistradoPorId,
                    FechaRegistro = DateTime.Now
                };

                _context.Prestamos.Add(prestamo);
                _context.SaveChanges();

                foreach (var detalleDTO in prestamoDTO.Detalles)
                {
                    var material = _context.Materiales.Find(detalleDTO.MaterialId);

                    if (material == null)
                    {
                        throw new Exception("Material no encontrado");
                    }

                    if (detalleDTO.CantidadPrestada <= 0)
                    {
                        throw new Exception("La cantidad prestada debe ser mayor a cero.");
                    }

                    if (material.StockActual < detalleDTO.CantidadPrestada)
                    {
                        throw new Exception("Stock insuficiente para el material: " + material.Nombre);
                    }

                    int stockAnterior = material.StockActual;
                    material.StockActual -= detalleDTO.CantidadPrestada;

                    var detalle = new DetallePrestamo
                    {
                        PrestamoId = prestamo.Id,
                        MaterialId = detalleDTO.MaterialId,
                        CantidadPrestada = detalleDTO.CantidadPrestada,
                        EstadoDevolucion = "Pendiente"
                    };

                    _context.DetallePrestamos.Add(detalle);

                    var movimiento = new MovimientoInventario
                    {
                        MaterialId = material.Id,
                        TipoMovimiento = "Prestamo",
                        Cantidad = detalleDTO.CantidadPrestada,
                        StockAnterior = stockAnterior,
                        StockNuevo = material.StockActual,
                        Observacion = "Préstamo registrado: " + prestamo.CodigoPrestamo,
                        FotoEvidencia = "",
                        RegistradoPorId = prestamoDTO.RegistradoPorId,
                        FechaMovimiento = DateTime.Now
                    };

                    _context.MovimientosInventario.Add(movimiento);
                }

                _context.SaveChanges();
                transaction.Commit();

                return ObtenerPorId(prestamo.Id)!;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }   
        }

        public PrestamoResponseDTO? CambiarEstado(int id, string estado)
        {
            var prestamoActualizado = _prestamoRepository.CambiarEstado(id, estado);

            if (prestamoActualizado == null)
            {
                return null;
            }

            return ObtenerPorId(id);
        }

        public PrestamoHistorialDTO? ObtenerHistorial(int id)
        {
            return _prestamoRepository.ObtenerHistorial(id);
        }
    }
}