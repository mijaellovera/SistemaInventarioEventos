namespace ProgramacioIV.DTOs.Devoluciones
{
    public class DevolucionResponseDTO
    {
        public int PrestamoId { get; set; }

        public int MaterialId { get; set; }

        public string Material { get; set; } = string.Empty;

        public int CantidadDevuelta { get; set; }

        public int StockActual { get; set; }

        public string EstadoPrestamo { get; set; } = string.Empty;

        public DateTime FechaDevolucion { get; set; }
    }
}