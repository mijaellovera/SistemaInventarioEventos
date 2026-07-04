using Microsoft.AspNetCore.Mvc;

namespace ProgramacioIV.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        public UploadController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost]
        public async Task<IActionResult> SubirImagen(IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
                return BadRequest("No se seleccionó ninguna imagen.");

            string uploads = Path.Combine(_environment.ContentRootPath, "Uploads");

            if (!Directory.Exists(uploads))
                Directory.CreateDirectory(uploads);

            string nombreArchivo =
                Guid.NewGuid().ToString() +
                Path.GetExtension(archivo.FileName);

            string rutaCompleta = Path.Combine(uploads, nombreArchivo);

            using (var stream = new FileStream(rutaCompleta, FileMode.Create))
            {
                await archivo.CopyToAsync(stream);
            }

            return Ok(new
            {
                ruta = "Uploads/" + nombreArchivo
            });
        }

        [HttpGet("{nombreArchivo}")]
        public IActionResult VerImagen(string nombreArchivo)
        {
            string uploads = Path.Combine(_environment.ContentRootPath, "Uploads");
            string rutaCompleta = Path.Combine(uploads, nombreArchivo);

            if (!System.IO.File.Exists(rutaCompleta))
            {
                return NotFound("Imagen no encontrada.");
            }

            string extension = Path.GetExtension(nombreArchivo).ToLower();

            string contentType = extension switch
            {
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".webp" => "image/webp",
                _ => "application/octet-stream"
            };

            return PhysicalFile(rutaCompleta, contentType);
        }
    }
}