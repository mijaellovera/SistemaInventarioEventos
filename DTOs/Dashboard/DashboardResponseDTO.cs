namespace ProgramacioIV.DTOs.Dashboard
{
    public class DashboardResponseDTO
    {
        public int TotalMateriales { get; set; }

        public int TotalPrestamos { get; set; }

        public int TotalMovimientos { get; set; }

        public int PrestamosPendientes { get; set; }

        public int PrestamosFinalizados { get; set; }

        public int MaterialesStockBajo { get; set; }

        public int MaterialesSinStock { get; set; }

        public int PrestamosHoy { get; set; }

        public int UsuariosRegistrados { get; set; }
    }
}