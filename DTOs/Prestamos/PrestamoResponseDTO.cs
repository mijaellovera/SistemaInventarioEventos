namespace ProgramacioIV.DTOs.Prestamos
{
    public class PrestamoResponseDTO
    {
        public int Id { get; set; }

        public string CodigoPrestamo { get; set; } = string.Empty;

        public string Solicitante { get; set; } = string.Empty;


        public DateTime FechaPrestamo { get; set; }

        public string Estado { get; set; } = string.Empty;

        public int RegistradoPorId { get; set; }

        public DateTime FechaRegistro { get; set; }

        public List<DetallePrestamoResponseDTO> Detalles { get; set; } = new();
    }
}