namespace ProgramacioIV.Models
{
    public class DetallePrestamo
    {
        public int Id { get; set; }

        public int PrestamoId { get; set; }

        public int MaterialId { get; set; }

        public int CantidadPrestada { get; set; }

        public int CantidadDevuelta { get; set; } = 0;
        public string EstadoDevolucion { get; set; } = "Pendiente";
        // Pendiente, Devuelto, Dañado, Extraviado

        public DateTime? FechaDevolucionReal { get; set; }

        public string ObservacionDevolucion { get; set; } = string.Empty;

        public string FotoEvidenciaDevolucion { get; set; } = string.Empty;

        public int? DevueltoPorId { get; set; }
    }
}