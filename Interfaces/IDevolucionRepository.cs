using ProgramacioIV.DTOs.Devoluciones;

namespace ProgramacioIV.Interfaces
{
    public interface IDevolucionRepository
    {
        DevolucionResponseDTO RegistrarDevolucion(DevolucionCreateDTO devolucionDTO);
    }
}