namespace ProgramacioIV.DTOs.Usuarios
{
    public class UsuarioCreateDTO
    {
        public string NombreCompleto { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;

        public string UsuarioLogin { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Rol { get; set; } = string.Empty;
    }
}