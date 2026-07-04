using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgramacioIV.Interfaces;

namespace ProgramacioIV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        public IActionResult ObtenerDashboard()
        {
            var dashboard = _dashboardService.ObtenerDashboard();
            return Ok(dashboard);
        }

        [HttpGet("materiales-mas-prestados")]
        public IActionResult ObtenerMaterialesMasPrestados()
        {
            var materiales = _dashboardService.ObtenerMaterialesMasPrestados();
            return Ok(materiales);
        }

        [HttpGet("alertas")]
        public IActionResult ObtenerAlertas()
        {
            var alertas = _dashboardService.ObtenerAlertas();
            return Ok(alertas);
        }

        [HttpGet("actividad-reciente")]
        public IActionResult ObtenerActividadReciente()
        {
            var actividad = _dashboardService.ObtenerActividadReciente();
            return Ok(actividad);
        }
    }
}