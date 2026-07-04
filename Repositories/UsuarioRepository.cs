using ProgramacioIV.Data;
using ProgramacioIV.Interfaces;
using ProgramacioIV.Models;

namespace ProgramacioIV.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Usuario> ObtenerTodos()
        {
            return _context.Usuarios.ToList();
        }

        public Usuario? ObtenerPorId(int id)
        {
            return _context.Usuarios.Find(id);
        }

        public Usuario Crear(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return usuario;
        }

        public Usuario? Actualizar(int id, Usuario usuarioActualizado)
        {
            var usuario = _context.Usuarios.Find(id);

            if (usuario == null)
            {
                return null;
            }

            usuario.NombreCompleto = usuarioActualizado.NombreCompleto;
            usuario.Telefono = usuarioActualizado.Telefono;
            usuario.UsuarioLogin = usuarioActualizado.UsuarioLogin;
            usuario.Password = usuarioActualizado.Password;
            usuario.Rol = usuarioActualizado.Rol;
            usuario.Estado = usuarioActualizado.Estado;

            _context.SaveChanges();

            return usuario;
        }

        public bool Eliminar(int id)
        {
            var usuario = _context.Usuarios.Find(id);

            if (usuario == null)
            {
                return false;
            }

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();

            return true;
        }

        public Usuario? ObtenerPorUsuarioLogin(string usuarioLogin)
        {
            return _context.Usuarios
                .FirstOrDefault(u => u.UsuarioLogin == usuarioLogin);
        }
    }
}