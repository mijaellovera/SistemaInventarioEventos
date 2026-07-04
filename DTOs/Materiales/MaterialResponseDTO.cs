namespace ProgramacioIV.DTOs.Materiales
{
    public class MaterialResponseDTO
    {
        public int Id { get; set; }

        public string Codigo { get; set; } = string.Empty;

        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public string ProveedorOrigen { get; set; } = string.Empty;

        public int StockActual { get; set; }

        public int StockMinimo { get; set; }

        public string Unidad { get; set; } = string.Empty;

        public string Imagen { get; set; } = string.Empty;

        public string Estado { get; set; } = string.Empty;

        public int RegistradoPorId { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}