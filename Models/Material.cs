namespace ProgramacioIV.Models
{
    public class Material
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

        public string Estado { get; set; } = "Activo";

        public int RegistradoPorId { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}