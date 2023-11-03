using DataAccess.Context;
using DataAccess.DTO;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiGuanaHospi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnfermedadController : ControllerBase
    {
        private readonly GuanaHospiContext _context;

        public EnfermedadController(GuanaHospiContext context) {
            _context = context;
        }


        [HttpGet]
        public List<Enfermedad> GetAllEnfermedad()
        {
            var enfermedades = _context.enfermedad
                // Llamando al sp de la db usando FromSqlInterpolated
                .FromSqlInterpolated($"EXEC SP_ObtenerEnfermedades")
                .ToList();

            return enfermedades;
        }


        [HttpGet("{id}")]
        public IActionResult GetEnfermedadById(int id)
        {
            var enfermedad = _context.enfermedad
            .FromSqlInterpolated($"EXEC SP_ObtenerEnfermedadPorId {id}")
            .AsEnumerable()
            .SingleOrDefault();

            if (enfermedad == null)
            {
                return NotFound();
            }

            return Ok(enfermedad);
        }

        [HttpPost]
        public IActionResult PostEnfermedad(EnfermedadDto enfermedadPostDto)
        {
            // objeto Doctor a partir del DTO
            var enfermedad = new Enfermedad
            {
                //ID_Doctor = doctorDTO.ID_Doctor,
                Nombre = enfermedadPostDto.Nombre,

            };

            _context.Database.ExecuteSqlInterpolated($"SP_InsertarEnfermedad {enfermedad.Nombre}");

            return Ok("Enfermedad creada exitosamente");
        }

        [HttpPut("{id}")]
        public IActionResult PutEnfermedad(int id, [FromBody] EnfermedadDto enfermedadDto)
        {
            var existingEnfermedad = _context.enfermedad.FirstOrDefault(d => d.Id_Enfermedad == id);

            if (existingEnfermedad == null)
            {
                return NotFound();
            }

            existingEnfermedad.Nombre = enfermedadDto.Nombre;
   
            _context.Database.ExecuteSqlInterpolated($"SP_ActualizarEnfermedad {id}, {existingEnfermedad.Nombre}");

            _context.SaveChanges();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteEnfermedad(int id)
        {
            var existingEnfermedad = _context.enfermedad.FirstOrDefault(d => d.Id_Enfermedad == id);

            if (existingEnfermedad == null)
            {
                // No encontrado
                return NotFound();
            }

            _context.Database.ExecuteSqlInterpolated($"SP_EliminarEnfermedad {id}");

            return NoContent();
        }
    }
}
