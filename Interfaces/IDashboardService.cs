using ProgramacioIV.DTOs.Dashboard;

namespace ProgramacioIV.Interfaces
{
    public interface IDashboardService
    {
        DashboardResponseDTO ObtenerDashboard();

        List<ActividadRecienteDTO> ObtenerActividadReciente();

        List<MaterialMasPrestadoDTO> ObtenerMaterialesMasPrestados();

        List<AlertaDashboardDTO> ObtenerAlertas();
    }
}