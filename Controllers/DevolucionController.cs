using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgramacioIV.DTOs.Devoluciones;
using ProgramacioIV.Interfaces;

namespace ProgramacioIV.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DevolucionController : ControllerBase
    {
        private readonly IDevolucionService _devolucionService;

        public DevolucionController(IDevolucionService devolucionService)
        {
            _devolucionService = devolucionService;
        }

        [Authorize(Roles = "Administrador,SuperAdministrador")]
        [HttpPost]
        public IActionResult RegistrarDevolucion(DevolucionCreateDTO devolucionDTO)
        {
            try
            {
                var respuesta = _devolucionService.RegistrarDevolucion(devolucionDTO);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}