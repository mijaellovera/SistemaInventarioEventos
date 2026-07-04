namespace ProgramacioIV.DTOs.Prestamos
{
    public class PrestamoHistorialDTO
    {
        public int Id { get; set; }

        public string CodigoPrestamo { get; set; } = string.Empty;

        public string Solicitante { get; set; } = string.Empty;

        public DateTime FechaPrestamo { get; set; }

        public string Estado { get; set; } = string.Empty;

        public DateTime FechaRegistro { get; set; }

        public List<DetallePrestamoHistorialDTO> Materiales { get; set; } = new();
    }
}