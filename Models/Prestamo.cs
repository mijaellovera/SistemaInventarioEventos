namespace ProgramacioIV.Models
{
    public class Prestamo
    {
        public int Id { get; set; }

        public string CodigoPrestamo { get; set; } = string.Empty;

        public string Solicitante { get; set; } = string.Empty;


        public DateTime FechaPrestamo { get; set; }


        public string Estado { get; set; } = "Pendiente";

        public int RegistradoPorId { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}