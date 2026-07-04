using Microsoft.AspNetCore.Mvc;
using ProgramacioIV.DTOs.Auth;
using ProgramacioIV.Interfaces;

namespace ProgramacioIV.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO loginDTO)
        {
            var respuesta = _authService.Login(loginDTO);

            if (respuesta == null)
            {
                return Unauthorized("Usuario o contraseña incorrectos.");
            }

            return Ok(respuesta);
        }
    }
}