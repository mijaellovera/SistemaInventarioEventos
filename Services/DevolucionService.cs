using ProgramacioIV.DTOs.Devoluciones;
using ProgramacioIV.Interfaces;

namespace ProgramacioIV.Services
{
    public class DevolucionService : IDevolucionService
    {
        private readonly IDevolucionRepository _devolucionRepository;

        public DevolucionService(IDevolucionRepository devolucionRepository)
        {
            _devolucionRepository = devolucionRepository;
        }

        public DevolucionResponseDTO RegistrarDevolucion(DevolucionCreateDTO devolucionDTO)
        {
            return _devolucionRepository.RegistrarDevolucion(devolucionDTO);
        }
    }
}