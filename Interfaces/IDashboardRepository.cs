using ProgramacioIV.DTOs.Dashboard;

namespace ProgramacioIV.Interfaces
{
    public interface IDashboardRepository
    {
        DashboardResponseDTO ObtenerDashboard();

        List<ActividadRecienteDTO> ObtenerActividadReciente();

        List<MaterialMasPrestadoDTO> ObtenerMaterialesMasPrestados();

        List<AlertaDashboardDTO> ObtenerAlertas();
    }
}