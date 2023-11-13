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
    public class EspecialidadController : ControllerBase
    {
        private readonly GuanaHospiContext _context;

        public EspecialidadController(GuanaHospiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Especialidad> GetAllEspecialidad()
        {
            var especialidades = _context.especialidad
                // Llamando al sp de la db usando FromSqlInterpolated
                .FromSqlInterpolated($"EXEC SP_ObtenerEspecialidades")
                .ToList();

            return especialidades;
        }

        [HttpGet("{id}")]
        public IActionResult GetEspecialidadById(int id)
        {
            var especialidad = _context.especialidad
            .FromSqlInterpolated($"EXEC SP_ObtenerEspecialidadPorId {id}")
            .AsEnumerable()
            .SingleOrDefault();

            if (especialidad == null)
            {
                return NotFound();
            }

            return Ok(especialidad);
        }

        [HttpPost]
        public IActionResult PostEnfermedad([FromBody] EspecialidadDto especialidadDto)
        {
            var especialidad = new Especialidad
            {
                NombreE = especialidadDto.NombreE,

            };

            _context.Database.ExecuteSqlInterpolated($"SP_InsertarEspecialidad {especialidad.NombreE}");

            return Ok("Especialidad creada exitosamente");
        }

        [HttpPut("{id}")]
        public IActionResult PutEspecialidad(int id, [FromBody] EspecialidadDto especialidadDto)
        {
            var existingEspecialidad = _context.especialidad.FirstOrDefault(d => d.ID_Especialidad == id);

            if (existingEspecialidad == null)
            {
                return NotFound();
            }

            existingEspecialidad.NombreE = especialidadDto.NombreE;

            _context.Database.ExecuteSqlInterpolated($"SP_ActualizarEspecialidad {id}, {existingEspecialidad.NombreE}");

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEspecialidad(int id)
        {
            var existingEspecialidad = _context.especialidad.FirstOrDefault(d => d.ID_Especialidad == id);

            if (existingEspecialidad == null)
            {
                // No encontrado
                return NotFound();
            }

            _context.Database.ExecuteSqlInterpolated($"SP_EliminarEspecialidad {id}");

            return NoContent();
        }
    }
}
