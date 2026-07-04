using ProgramacioIV.DTOs.Dashboard;
using ProgramacioIV.Interfaces;

namespace ProgramacioIV.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public DashboardResponseDTO ObtenerDashboard()
        {
            return _dashboardRepository.ObtenerDashboard();
        }

        public List<ActividadRecienteDTO> ObtenerActividadReciente()
        {
            return _dashboardRepository.ObtenerActividadReciente();
        }

        public List<MaterialMasPrestadoDTO> ObtenerMaterialesMasPrestados()
        {
            return _dashboardRepository.ObtenerMaterialesMasPrestados();
        }

        public List<AlertaDashboardDTO> ObtenerAlertas()
        {
            return _dashboardRepository.ObtenerAlertas();
        }
    }
}