namespace ProgramacioIV.DTOs.Devoluciones
{
    public class DevolucionCreateDTO
    {
        public int PrestamoId { get; set; }

        public int MaterialId { get; set; }

        public int CantidadDevuelta { get; set; }

        public int RegistradoPorId { get; set; }

        public string Observacion { get; set; } = string.Empty;

        public string FotoEvidencia { get; set; } = string.Empty;
    }
}