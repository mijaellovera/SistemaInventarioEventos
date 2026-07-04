using ProgramacioIV.DTOs.Movimientos;

namespace ProgramacioIV.Interfaces
{
    public interface IMovimientoInventarioRepository
    {
        List<MovimientoInventarioResponseDTO> ObtenerTodos();

        MovimientoInventarioResponseDTO? ObtenerPorId(int id);

        List<MovimientoInventarioResponseDTO> ObtenerPorMaterial(int materialId);
    }
}