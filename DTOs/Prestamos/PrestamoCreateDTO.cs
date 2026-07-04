namespace ProgramacioIV.DTOs.Prestamos
{
    public class PrestamoCreateDTO
    {

        public string Solicitante { get; set; } = string.Empty;


        public DateTime FechaPrestamo { get; set; }

        public int RegistradoPorId { get; set; }

        public List<DetallePrestamoCreateDTO> Detalles { get; set; } = new();
    }
}