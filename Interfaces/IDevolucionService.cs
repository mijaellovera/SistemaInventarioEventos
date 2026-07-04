using ProgramacioIV.DTOs.Devoluciones;

namespace ProgramacioIV.Interfaces
{
    public interface IDevolucionService
    {
        DevolucionResponseDTO RegistrarDevolucion(DevolucionCreateDTO devolucionDTO);
    }
}