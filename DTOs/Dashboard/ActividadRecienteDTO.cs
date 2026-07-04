namespace ProgramacioIV.DTOs.Dashboard
{
    public class ActividadRecienteDTO
    {
        public string Tipo { get; set; } = string.Empty;

        public string Usuario { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public DateTime Fecha { get; set; }
    }
}