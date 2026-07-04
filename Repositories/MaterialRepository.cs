using ProgramacioIV.Data;
using ProgramacioIV.Interfaces;
using ProgramacioIV.Models;

namespace ProgramacioIV.Repositories
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly AppDbContext _context;

        public MaterialRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Material> ObtenerTodos()
        {
            return _context.Materiales.ToList();
        }

        public Material? ObtenerPorId(int id)
        {
            return _context.Materiales.Find(id);
        }

        public Material Crear(Material material)
        {
            _context.Materiales.Add(material);
            _context.SaveChanges();

            return material;
        }

        public Material? Actualizar(int id, Material materialActualizado)
        {
            var material = _context.Materiales.Find(id);

            if (material == null)
            {
                return null;
            }

            material.Codigo = materialActualizado.Codigo;
            material.Nombre = materialActualizado.Nombre;
            material.Descripcion = materialActualizado.Descripcion;
            material.ProveedorOrigen = materialActualizado.ProveedorOrigen;
            material.StockActual = materialActualizado.StockActual;
            material.StockMinimo = materialActualizado.StockMinimo;
            material.Unidad = materialActualizado.Unidad;
            material.Imagen = materialActualizado.Imagen;
            material.Estado = materialActualizado.Estado;

            _context.SaveChanges();

            return material;
        }

        public bool Eliminar(int id)
        {
            var material = _context.Materiales.Find(id);

            if (material == null)
            {
                return false;
            }

            _context.Materiales.Remove(material);
            _context.SaveChanges();

            return true;
        }
    }
}