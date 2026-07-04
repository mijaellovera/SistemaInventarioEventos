namespace ProgramacioIV.DTOs.Auth
{
    public class LoginResponseDTO
    {
        public string Token { get; set; } = string.Empty;

        public string NombreCompleto { get; set; } = string.Empty;

        public string UsuarioLogin { get; set; } = string.Empty;

        public string Rol { get; set; } = string.Empty;
    }
}