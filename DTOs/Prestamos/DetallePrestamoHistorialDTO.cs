namespace ProgramacioIV.DTOs.Prestamos
{
    public class DetallePrestamoHistorialDTO
    {
        public int MaterialId { get; set; }

        public string Material { get; set; } = string.Empty;

        public int CantidadPrestada { get; set; }

        public int CantidadDevuelta { get; set; }

        public int CantidadPendiente { get; set; }

        public string EstadoDevolucion { get; set; } = string.Empty;
    }
}