using ProgramacioIV.DTOs.Movimientos;
using ProgramacioIV.Interfaces;

namespace ProgramacioIV.Services
{
    public class MovimientoInventarioService : IMovimientoInventarioService
    {
        private readonly IMovimientoInventarioRepository _movimientoRepository;

        public MovimientoInventarioService(IMovimientoInventarioRepository movimientoRepository)
        {
            _movimientoRepository = movimientoRepository;
        }

        public List<MovimientoInventarioResponseDTO> ObtenerTodos()
        {
            return _movimientoRepository.ObtenerTodos();
        }

        public MovimientoInventarioResponseDTO? ObtenerPorId(int id)
        {
            return _movimientoRepository.ObtenerPorId(id);
        }

        public List<MovimientoInventarioResponseDTO> ObtenerPorMaterial(int materialId)
        {
            return _movimientoRepository.ObtenerPorMaterial(materialId);
        }
    }
}