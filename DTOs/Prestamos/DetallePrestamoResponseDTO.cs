namespace ProgramacioIV.DTOs.Prestamos
{
    public class DetallePrestamoResponseDTO
    {
        public int Id { get; set; }
        public int MaterialId { get; set; }
        public string? MaterialNombre { get; set; }
        public int CantidadPrestada { get; set; }

        public int CantidadDevuelta { get; set; }
        public string EstadoDevolucion { get; set; } = string.Empty;

        public string FotoEvidenciaDevolucion { get; set; } = string.Empty;
        public string ObservacionDevolucion { get; set; } = string.Empty;

        public DateTime? FechaDevolucionReal { get; set; }
        public int? DevueltoPorId { get; set; }

        public string? DevueltoPorNombre { get; set; }
    }
}