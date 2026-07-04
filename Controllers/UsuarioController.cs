using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgramacioIV.DTOs.Usuarios;
using ProgramacioIV.Interfaces;
using System.Security.Claims;

namespace ProgramacioIV.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        private string RolActual()
        {
            return User.FindFirst(ClaimTypes.Role)?.Value ?? "";
        }

        [HttpGet]
        public IActionResult ObtenerUsuarios()
        {
            return Ok(_usuarioService.ObtenerTodos());
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerUsuarioPorId(int id)
        {
            var usuario = _usuarioService.ObtenerPorId(id);

            if (usuario == null)
            {
                return NotFound("Usuario no encontrado");
            }

            return Ok(usuario);
        }

        [HttpPost]
        public IActionResult CrearUsuario(UsuarioCreateDTO usuarioDTO)
        {
            if (RolActual() == "Administrador" &&
        usuarioDTO.Rol == "SuperAdministrador")
            {
                return Forbid("No tiene permisos para crear un SuperAdministrador.");
            }

            var usuarioCreado = _usuarioService.Crear(usuarioDTO);

            return Ok(usuarioCreado);
        }

        [HttpPut("{id}")]
        public IActionResult EditarUsuario(int id, UsuarioUpdateDTO usuarioDTO)
        {
            var usuarioExistente = _usuarioService.ObtenerPorId(id);

            if (usuarioExistente == null)
            {
                return NotFound("Usuario no encontrado");
            }

            if (RolActual() == "Administrador" &&
                usuarioExistente.Rol == "SuperAdministrador")
            {
                return Forbid("No tiene permisos para modificar un SuperAdministrador.");
            }

            if (RolActual() == "Administrador" &&
                usuarioDTO.Rol == "SuperAdministrador")
            {
                return Forbid("No tiene permisos para asignar el rol SuperAdministrador.");
            }

            var usuarioActualizado = _usuarioService.Actualizar(id, usuarioDTO);

            return Ok(usuarioActualizado);
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarUsuario(int id)
        {
            if (RolActual() != "SuperAdministrador")
            {
                return Forbid("Solo el SuperAdministrador puede eliminar usuarios.");
            }

            var eliminado = _usuarioService.Eliminar(id);

            if (!eliminado)
            {
                return NotFound("Usuario no encontrado");
            }

            return Ok("Usuario eliminado correctamente");
        }
    }
}