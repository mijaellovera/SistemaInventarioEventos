using ProgramacioIV.DTOs.Prestamos;

namespace ProgramacioIV.Interfaces
{
    public interface IPrestamoService
    {
        List<PrestamoResponseDTO> ObtenerTodos();

        PrestamoResponseDTO? ObtenerPorId(int id);

        PrestamoResponseDTO Crear(PrestamoCreateDTO prestamoDTO);

        PrestamoResponseDTO? CambiarEstado(int id, string estado);

        PrestamoHistorialDTO? ObtenerHistorial(int id);
    }
}