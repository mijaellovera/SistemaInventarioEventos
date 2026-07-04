using ProgramacioIV.DTOs.Materiales;
using ProgramacioIV.Interfaces;
using ProgramacioIV.Models;

namespace ProgramacioIV.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepository _materialRepository;

        public MaterialService(IMaterialRepository materialRepository)
        {
            _materialRepository = materialRepository;
        }

        public List<MaterialResponseDTO> ObtenerTodos()
        {
            var materiales = _materialRepository.ObtenerTodos();

            return materiales.Select(m => new MaterialResponseDTO
            {
                Id = m.Id,
                Codigo = m.Codigo,
                Nombre = m.Nombre,
                Descripcion = m.Descripcion,
                ProveedorOrigen = m.ProveedorOrigen,
                StockActual = m.StockActual,
                StockMinimo = m.StockMinimo,
                Unidad = m.Unidad,
                Imagen = m.Imagen,
                Estado = m.Estado,
                RegistradoPorId = m.RegistradoPorId,
                FechaRegistro = m.FechaRegistro
            }).ToList();
        }

        public MaterialResponseDTO? ObtenerPorId(int id)
        {
            var material = _materialRepository.ObtenerPorId(id);

            if (material == null)
            {
                return null;
            }

            return new MaterialResponseDTO
            {
                Id = material.Id,
                Codigo = material.Codigo,
                Nombre = material.Nombre,
                Descripcion = material.Descripcion,
                ProveedorOrigen = material.ProveedorOrigen,
                StockActual = material.StockActual,
                StockMinimo = material.StockMinimo,
                Unidad = material.Unidad,
                Imagen = material.Imagen,
                Estado = material.Estado,
                RegistradoPorId = material.RegistradoPorId,
                FechaRegistro = material.FechaRegistro
            };
        }

        public MaterialResponseDTO Crear(MaterialCreateDTO materialDTO)
        {
            var material = new Material
            {
                Codigo = materialDTO.Codigo,
                Nombre = materialDTO.Nombre,
                Descripcion = materialDTO.Descripcion,
                ProveedorOrigen = materialDTO.ProveedorOrigen,
                StockActual = materialDTO.StockActual,
                StockMinimo = materialDTO.StockMinimo,
                Unidad = materialDTO.Unidad,
                Imagen = materialDTO.Imagen,
                RegistradoPorId = materialDTO.RegistradoPorId,
                Estado = "Activo",
                FechaRegistro = DateTime.Now
            };

            var materialCreado = _materialRepository.Crear(material);

            return new MaterialResponseDTO
            {
                Id = materialCreado.Id,
                Codigo = materialCreado.Codigo,
                Nombre = materialCreado.Nombre,
                Descripcion = materialCreado.Descripcion,
                ProveedorOrigen = materialCreado.ProveedorOrigen,
                StockActual = materialCreado.StockActual,
                StockMinimo = materialCreado.StockMinimo,
                Unidad = materialCreado.Unidad,
                Imagen = materialCreado.Imagen,
                Estado = materialCreado.Estado,
                RegistradoPorId = materialCreado.RegistradoPorId,
                FechaRegistro = materialCreado.FechaRegistro
            };
        }

        public MaterialResponseDTO? Actualizar(int id, MaterialUpdateDTO materialDTO)
        {
            var material = new Material
            {
                Codigo = materialDTO.Codigo,
                Nombre = materialDTO.Nombre,
                Descripcion = materialDTO.Descripcion,
                ProveedorOrigen = materialDTO.ProveedorOrigen,
                StockActual = materialDTO.StockActual,
                StockMinimo = materialDTO.StockMinimo,
                Unidad = materialDTO.Unidad,
                Imagen = materialDTO.Imagen,
                Estado = materialDTO.Estado
            };

            var materialActualizado = _materialRepository.Actualizar(id, material);

            if (materialActualizado == null)
            {
                return null;
            }

            return new MaterialResponseDTO
            {
                Id = materialActualizado.Id,
                Codigo = materialActualizado.Codigo,
                Nombre = materialActualizado.Nombre,
                Descripcion = materialActualizado.Descripcion,
                ProveedorOrigen = materialActualizado.ProveedorOrigen,
                StockActual = materialActualizado.StockActual,
                StockMinimo = materialActualizado.StockMinimo,
                Unidad = materialActualizado.Unidad,
                Imagen = materialActualizado.Imagen,
                Estado = materialActualizado.Estado,
                RegistradoPorId = materialActualizado.RegistradoPorId,
                FechaRegistro = materialActualizado.FechaRegistro
            };
        }

        public bool Eliminar(int id)
        {
            return _materialRepository.Eliminar(id);
        }
    }
}