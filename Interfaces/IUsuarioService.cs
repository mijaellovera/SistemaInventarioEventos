using ProgramacioIV.DTOs.Usuarios;

namespace ProgramacioIV.Interfaces
{
    public interface IUsuarioService
    {
        List<UsuarioResponseDTO> ObtenerTodos();

        UsuarioResponseDTO? ObtenerPorId(int id);

        UsuarioResponseDTO Crear(UsuarioCreateDTO usuarioDTO);

        UsuarioResponseDTO? Actualizar(int id, UsuarioUpdateDTO usuarioDTO);

        bool Eliminar(int id);
    }
}