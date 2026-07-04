using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgramacioIV.Interfaces;

namespace ProgramacioIV.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MovimientoInventarioController : ControllerBase
    {
        private readonly IMovimientoInventarioService _movimientoService;

        public MovimientoInventarioController(IMovimientoInventarioService movimientoService)
        {
            _movimientoService = movimientoService;
        }

        [HttpGet]
        public IActionResult ObtenerMovimientos()
        {
            return Ok(_movimientoService.ObtenerTodos());
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerMovimientoPorId(int id)
        {
            var movimiento = _movimientoService.ObtenerPorId(id);

            if (movimiento == null)
            {
                return NotFound("Movimiento no encontrado");
            }

            return Ok(movimiento);
        }

        [HttpGet("material/{materialId}")]
        public IActionResult ObtenerMovimientosPorMaterial(int materialId)
        {
            return Ok(_movimientoService.ObtenerPorMaterial(materialId));
        }
    }
}