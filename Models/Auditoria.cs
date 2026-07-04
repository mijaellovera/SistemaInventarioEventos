namespace ProgramacioIV.Models
{
    public class Auditoria
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        public string Accion { get; set; } = string.Empty;

        public string Modulo { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public string ValorAnterior { get; set; } = string.Empty;

        public string ValorNuevo { get; set; } = string.Empty;

        public DateTime FechaHora { get; set; } = DateTime.Now;
    }
}