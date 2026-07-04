using ProgramacioIV.Models;

namespace ProgramacioIV.Interfaces
{
    public interface IMaterialRepository
    {
        List<Material> ObtenerTodos();

        Material? ObtenerPorId(int id);

        Material Crear(Material material);

        Material? Actualizar(int id, Material material);

        bool Eliminar(int id);
    }
}