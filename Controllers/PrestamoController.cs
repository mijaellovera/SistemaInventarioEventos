using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgramacioIV.DTOs.Prestamos;
using ProgramacioIV.Interfaces;

namespace ProgramacioIV.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PrestamoController : ControllerBase
    {
        private readonly IPrestamoService _prestamoService;

        public PrestamoController(IPrestamoService prestamoService)
        {
            _prestamoService = prestamoService;
        }

        [HttpGet]
        public IActionResult ObtenerPrestamos()
        {
            return Ok(_prestamoService.ObtenerTodos());
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerPrestamoPorId(int id)
        {
            var prestamo = _prestamoService.ObtenerPorId(id);

            if (prestamo == null)
            {
                return NotFound("Préstamo no encontrado");
            }

            return Ok(prestamo);
        }

        [HttpGet("{id}/historial")]
        public IActionResult ObtenerHistorial(int id)
        {
            var historial = _prestamoService.ObtenerHistorial(id);

            if (historial == null)
            {
                return NotFound("Préstamo no encontrado.");
            }

            return Ok(historial);
        }

        [Authorize(Roles = "Administrador,SuperAdministrador")]
        [HttpPost]
        public IActionResult CrearPrestamo(PrestamoCreateDTO prestamoDTO)
        {
            try
            {
                var prestamoCreado = _prestamoService.Crear(prestamoDTO);
                return Ok(prestamoCreado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Administrador,SuperAdministrador")]
        [HttpPut("{id}/estado")]
        public IActionResult CambiarEstado(int id, string estado)
        {
            var prestamoActualizado = _prestamoService.CambiarEstado(id, estado);

            if (prestamoActualizado == null)
            {
                return NotFound("Préstamo no encontrado");
            }

            return Ok(prestamoActualizado);
        }
    }
}