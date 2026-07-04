using ProgramacioIV.Data;
using ProgramacioIV.DTOs.Dashboard;
using ProgramacioIV.Interfaces;

namespace ProgramacioIV.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly AppDbContext _context;

        public DashboardRepository(AppDbContext context)
        {
            _context = context;
        }

        public DashboardResponseDTO ObtenerDashboard()
        {
            return new DashboardResponseDTO
            {
                TotalMateriales = _context.Materiales.Count(),

                TotalPrestamos = _context.Prestamos.Count(),

                TotalMovimientos = _context.MovimientosInventario.Count(),

                PrestamosPendientes =
                    _context.Prestamos.Count(p => p.Estado == "Pendiente"),

                PrestamosFinalizados =
                    _context.Prestamos.Count(p => p.Estado == "Finalizado"),

                // NUEVOS INDICADORES

                MaterialesStockBajo =
                    _context.Materiales.Count(m => m.StockActual <= 5 && m.StockActual > 0),

                MaterialesSinStock =
                    _context.Materiales.Count(m => m.StockActual == 0),

                PrestamosHoy =
                    _context.Prestamos.Count(p =>
                        p.FechaPrestamo.Date == DateTime.Today),

                UsuariosRegistrados =
                    _context.Usuarios.Count()
            };
        }

        public List<ActividadRecienteDTO> ObtenerActividadReciente()
        {
            return _context.MovimientosInventario
                .OrderByDescending(m => m.FechaMovimiento)
                .Take(5)
                .Select(m => new ActividadRecienteDTO
                {
                    Tipo = m.TipoMovimiento,

                    Usuario = _context.Usuarios
                        .Where(u => u.Id == m.RegistradoPorId)
                        .Select(u => u.NombreCompleto)
                        .FirstOrDefault() ?? "Usuario no encontrado",

                    Descripcion =
                        m.TipoMovimiento + " de " +
                        m.Cantidad + " unidad(es) del material " +
                        (_context.Materiales
                            .Where(mat => mat.Id == m.MaterialId)
                            .Select(mat => mat.Nombre)
                            .FirstOrDefault() ?? "Sin nombre"),

                    Fecha = m.FechaMovimiento
                })
                .ToList();
        }

        public List<MaterialMasPrestadoDTO> ObtenerMaterialesMasPrestados()
        {
            return _context.DetallePrestamos
                .GroupBy(d => d.MaterialId)
                .Select(g => new MaterialMasPrestadoDTO
                {
                    Material = _context.Materiales
                        .Where(m => m.Id == g.Key)
                        .Select(m => m.Nombre)
                        .FirstOrDefault() ?? "Sin nombre",

                    Cantidad = g.Sum(x => x.CantidadPrestada)
                })
                .OrderByDescending(x => x.Cantidad)
                .Take(5)
                .ToList();
        }

        public List<AlertaDashboardDTO> ObtenerAlertas()
        {
            var alertas = new List<AlertaDashboardDTO>();

            // Materiales sin stock (Máximo 5)
            var sinStock = _context.Materiales
                .Where(m => m.StockActual == 0)
                .OrderBy(m => m.Nombre)
                .Take(5)
                .ToList();

            foreach (var material in sinStock)
            {
                alertas.Add(new AlertaDashboardDTO
                {
                    Tipo = "Peligro",
                    Titulo = "Material sin stock",
                    Mensaje = $"{material.Nombre} está agotado."
                });
            }

            // Materiales con stock bajo (Máximo 5)
            var stockBajo = _context.Materiales
                .Where(m => m.StockActual > 0 &&
                            m.StockActual <= m.StockMinimo)
                .OrderBy(m => m.StockActual)
                .Take(5)
                .ToList();

            foreach (var material in stockBajo)
            {
                alertas.Add(new AlertaDashboardDTO
                {
                    Tipo = "Advertencia",
                    Titulo = "Stock bajo",
                    Mensaje = $"{material.Nombre}: {material.StockActual} disponibles (mínimo {material.StockMinimo})."
                });
            }

            // Resumen de préstamos pendientes
            int pendientes = _context.Prestamos
                .Count(p => p.Estado == "Pendiente");

            if (pendientes > 0)
            {
                alertas.Add(new AlertaDashboardDTO
                {
                    Tipo = "Informacion",
                    Titulo = "Préstamos pendientes",
                    Mensaje = $"Existen {pendientes} préstamo(s) pendientes de devolución."
                });
            }

            return alertas;
        }
    }
}