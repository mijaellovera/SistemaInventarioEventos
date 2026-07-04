using ProgramacioIV.DTOs.Materiales;

namespace ProgramacioIV.Interfaces
{
    public interface IMaterialService
    {
        List<MaterialResponseDTO> ObtenerTodos();

        MaterialResponseDTO? ObtenerPorId(int id);

        MaterialResponseDTO Crear(MaterialCreateDTO materialDTO);

        MaterialResponseDTO? Actualizar(int id, MaterialUpdateDTO materialDTO);

        bool Eliminar(int id);
    }
}