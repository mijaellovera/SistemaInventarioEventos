namespace ProgramacioIV.DTOs.Movimientos
{
    public class MovimientoInventarioResponseDTO
    {
        public int Id { get; set; }

        public int MaterialId { get; set; }

        public string Material { get; set; } = string.Empty;

        public string TipoMovimiento { get; set; } = string.Empty;

        public int Cantidad { get; set; }

        public int StockAnterior { get; set; }

        public int StockNuevo { get; set; }

        public string Observacion { get; set; } = string.Empty;

        public string FotoEvidencia { get; set; } = string.Empty;

        public int RegistradoPorId { get; set; }

        public DateTime FechaMovimiento { get; set; }
    }
}