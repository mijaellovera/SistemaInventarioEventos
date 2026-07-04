using ProgramacioIV.DTOs.Prestamos;
using ProgramacioIV.Models;

namespace ProgramacioIV.Interfaces
{
    public interface IPrestamoRepository
    {
        List<Prestamo> ObtenerTodos();

        Prestamo? ObtenerPorId(int id);

        Prestamo Crear(Prestamo prestamo);

        Prestamo? CambiarEstado(int id, string estado);

        PrestamoHistorialDTO? ObtenerHistorial(int id);
    }
}