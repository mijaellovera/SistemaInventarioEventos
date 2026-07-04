namespace ProgramacioIV.DTOs.Usuarios
{
    public class UsuarioResponseDTO
    {
        public int Id { get; set; }

        public string NombreCompleto { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;

        public string UsuarioLogin { get; set; } = string.Empty;

        public string Rol { get; set; } = string.Empty;

        public bool Estado { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}