using ProgramacioIV.DTOs.Usuarios;
using ProgramacioIV.Interfaces;
using ProgramacioIV.Models;

namespace ProgramacioIV.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public List<UsuarioResponseDTO> ObtenerTodos()
        {
            var usuarios = _usuarioRepository.ObtenerTodos();

            return usuarios.Select(u => new UsuarioResponseDTO
            {
                Id = u.Id,
                NombreCompleto = u.NombreCompleto,
                Telefono = u.Telefono,
                UsuarioLogin = u.UsuarioLogin,
                Rol = u.Rol,
                Estado = u.Estado,
                FechaRegistro = u.FechaRegistro
            }).ToList();
        }

        public UsuarioResponseDTO? ObtenerPorId(int id)
        {
            var usuario = _usuarioRepository.ObtenerPorId(id);

            if (usuario == null)
            {
                return null;
            }

            return new UsuarioResponseDTO
            {
                Id = usuario.Id,
                NombreCompleto = usuario.NombreCompleto,
                Telefono = usuario.Telefono,
                UsuarioLogin = usuario.UsuarioLogin,
                Rol = usuario.Rol,
                Estado = usuario.Estado,
                FechaRegistro = usuario.FechaRegistro
            };
        }

        private bool RolValido(string rol)
        {
            return rol == "SuperAdministrador"
                || rol == "Administrador"
                || rol == "Operador";
        }

        public UsuarioResponseDTO Crear(UsuarioCreateDTO usuarioDTO)
        {
            if (!RolValido(usuarioDTO.Rol))
            {
                throw new Exception("Rol no válido.");
            }
            var usuario = new Usuario
            {
                NombreCompleto = usuarioDTO.NombreCompleto,
                Telefono = usuarioDTO.Telefono,
                UsuarioLogin = usuarioDTO.UsuarioLogin,
                Password = BCrypt.Net.BCrypt.HashPassword(usuarioDTO.Password),
                Rol = usuarioDTO.Rol,
                Estado = true,
                FechaRegistro = DateTime.Now
            };

            var usuarioCreado = _usuarioRepository.Crear(usuario);

            return new UsuarioResponseDTO
            {
                Id = usuarioCreado.Id,
                NombreCompleto = usuarioCreado.NombreCompleto,
                Telefono = usuarioCreado.Telefono,
                UsuarioLogin = usuarioCreado.UsuarioLogin,
                Rol = usuarioCreado.Rol,
                Estado = usuarioCreado.Estado,
                FechaRegistro = usuarioCreado.FechaRegistro
            };
        }

        public UsuarioResponseDTO? Actualizar(int id, UsuarioUpdateDTO usuarioDTO)
        {

            if (!RolValido(usuarioDTO.Rol))
            {
                throw new Exception("Rol no válido.");
            }
            var usuario = new Usuario
            {
                NombreCompleto = usuarioDTO.NombreCompleto,
                Telefono = usuarioDTO.Telefono,
                UsuarioLogin = usuarioDTO.UsuarioLogin,
                Password = usuarioDTO.Password,
                Rol = usuarioDTO.Rol,
                Estado = usuarioDTO.Estado
            };

            var usuarioActualizado = _usuarioRepository.Actualizar(id, usuario);

            if (usuarioActualizado == null)
            {
                return null;
            }

            return new UsuarioResponseDTO
            {
                Id = usuarioActualizado.Id,
                NombreCompleto = usuarioActualizado.NombreCompleto,
                Telefono = usuarioActualizado.Telefono,
                UsuarioLogin = usuarioActualizado.UsuarioLogin,
                Rol = usuarioActualizado.Rol,
                Estado = usuarioActualizado.Estado,
                FechaRegistro = usuarioActualizado.FechaRegistro
            };
        }

        public bool Eliminar(int id)
        {
            return _usuarioRepository.Eliminar(id);
        }
    }
}