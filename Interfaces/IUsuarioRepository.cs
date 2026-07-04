using ProgramacioIV.Models;

namespace ProgramacioIV.Interfaces
{
    public interface IUsuarioRepository
    {
        List<Usuario> ObtenerTodos();

        Usuario? ObtenerPorId(int id);

        Usuario Crear(Usuario usuario);

        Usuario? Actualizar(int id, Usuario usuario);

        bool Eliminar(int id);

        Usuario? ObtenerPorUsuarioLogin(string usuarioLogin);
    }
}