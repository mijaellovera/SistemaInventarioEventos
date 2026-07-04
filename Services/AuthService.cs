using Microsoft.IdentityModel.Tokens;
using ProgramacioIV.Data;
using ProgramacioIV.DTOs.Auth;
using ProgramacioIV.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProgramacioIV.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public LoginResponseDTO? Login(LoginDTO loginDTO)
        {
            var usuario = _context.Usuarios
    .FirstOrDefault(u =>
        u.UsuarioLogin == loginDTO.UsuarioLogin
    );

            if (usuario == null)
            {
                return null;
            }

            bool passwordValido;

            if (usuario.Password.StartsWith("$2a$") ||
                usuario.Password.StartsWith("$2b$") ||
                usuario.Password.StartsWith("$2y$"))
            {
                passwordValido = BCrypt.Net.BCrypt.Verify(
                    loginDTO.Password,
                    usuario.Password
                );
            }
            else
            {
                passwordValido = usuario.Password == loginDTO.Password;

                if (passwordValido)
                {
                    usuario.Password = BCrypt.Net.BCrypt.HashPassword(loginDTO.Password);
                    _context.SaveChanges();
                }
            }

            if (!passwordValido)
            {
                return null;
            }

            if (!usuario.Estado)
            {
                throw new Exception("Su cuenta se encuentra desactivada. Comuníquese con el administrador.");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.UsuarioLogin),
                new Claim(ClaimTypes.Role, usuario.Rol)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
            );

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(
                    Convert.ToDouble(_configuration["Jwt:DurationInMinutes"])
                ),
                signingCredentials: credentials
            );

            return new LoginResponseDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                NombreCompleto = usuario.NombreCompleto,
                UsuarioLogin = usuario.UsuarioLogin,
                Rol = usuario.Rol
            };
        }
    }
}