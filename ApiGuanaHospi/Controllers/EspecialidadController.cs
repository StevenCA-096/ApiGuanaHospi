using DataAccess.Context;
using DataAccess.DTO;
using DataAccess.Models;
using DataAccess.RequestObjects;
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
        public List<EspecialidadRequest> GetAllEspecialidad()
        {
            var especialidades = _context.especialidadRequest
                // Llamando al sp de la db usando FromSqlInterpolated
                .FromSqlInterpolated($"EXEC SP_ObtenerEspecialidades")
                .ToList();

            return especialidades;
        }

        [HttpGet("{id}")]
        public IActionResult GetEspecialidadById(int id)
        {
            var especialidad = _context.especialidadRequest
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
        public IActionResult PostEnfermedad(int idUsuario, [FromBody] EspecialidadDto especialidadDto)
        {
            _context.Database.OpenConnection();
            _context.Database.ExecuteSqlRaw($"EXEC sp_set_session_context 'user_id', {idUsuario};");
            var especialidad = new Especialidad
            {
                Nombre = especialidadDto.NombreE,

            };

            _context.Database.ExecuteSqlInterpolated($"SP_InsertarEspecialidad {especialidad.Nombre}");
            _context.Database.CloseConnection();
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

            existingEspecialidad.Nombre = especialidadDto.NombreE;

            _context.Database.ExecuteSqlInterpolated($"SP_ActualizarEspecialidad {id}, {existingEspecialidad.Nombre}");

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
