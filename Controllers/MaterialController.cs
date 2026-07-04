using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgramacioIV.DTOs.Materiales;
using ProgramacioIV.Interfaces;

namespace ProgramacioIV.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialService _materialService;

        public MaterialController(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        [HttpGet]
        public IActionResult ObtenerMateriales()
        {
            return Ok(_materialService.ObtenerTodos());
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerMaterialPorId(int id)
        {
            var material = _materialService.ObtenerPorId(id);

            if (material == null)
            {
                return NotFound("Material no encontrado");
            }

            return Ok(material);
        }

        [Authorize(Roles = "Operador,Administrador,SuperAdministrador")]
        [HttpPost]
        public IActionResult CrearMaterial(MaterialCreateDTO materialDTO)
        {
            var materialCreado = _materialService.Crear(materialDTO);

            return Ok(materialCreado);
        }

        [Authorize(Roles = "Operador,Administrador,SuperAdministrador")]
        [HttpPut("{id}")]
        public IActionResult EditarMaterial(int id, MaterialUpdateDTO materialDTO)
        {
            var materialActualizado = _materialService.Actualizar(id, materialDTO);

            if (materialActualizado == null)
            {
                return NotFound("Material no encontrado");
            }

            return Ok(materialActualizado);
        }

        [Authorize(Roles = "Administrador,SuperAdministrador")]
        [HttpDelete("{id}")]
        public IActionResult EliminarMaterial(int id)
        {
            var eliminado = _materialService.Eliminar(id);

            if (!eliminado)
            {
                return NotFound("Material no encontrado");
            }

            return Ok("Material eliminado correctamente");
        }
    }
}