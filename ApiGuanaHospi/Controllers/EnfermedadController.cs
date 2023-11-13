using DataAccess.Context;
using DataAccess.DTO;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

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
        public IActionResult GetAllEnfermedad()
        {
            try
            {
                var enfermedades = _context.enfermedad
                    .FromSqlInterpolated($"EXEC SP_ObtenerEnfermedades")
                    .ToList();

                return Ok(enfermedades);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno -> " + ex.Message);
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetEnfermedadById(int id)
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }


        [HttpPost]
        public IActionResult PostEnfermedad([FromBody] EnfermedadDto enfermedadPostDto)
        {
            try
            {

            var enfermedad = new Enfermedad
            {
                Nombre = enfermedadPostDto.Nombre,
            };

            var result = _context.Database.ExecuteSqlInterpolated($"SP_InsertarEnfermedad {enfermedad.Nombre}");

            return CreatedAtAction(nameof(GetEnfermedadById), new { id = enfermedad.Id_Enfermedad }, enfermedad);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al crear enfermedad. -> " + ex);
            }

        }


        [HttpPut("{id}")]
        public IActionResult PutEnfermedad(int id, [FromBody] EnfermedadDto enfermedadDto)
        {
            try
            {

            var existingEnfermedad = _context.enfermedad.FirstOrDefault(d => d.Id_Enfermedad == id);

            if (existingEnfermedad == null)
            {
                return NotFound();
            }

            existingEnfermedad.Nombre = enfermedadDto.Nombre;

            var result = _context.Database.ExecuteSqlInterpolated($"SP_ActualizarEnfermedad {id}, {existingEnfermedad.Nombre}");

           
                return NoContent();

            }
            catch (Exception ex)
            {
                // Manejar el error según tus necesidades
                return StatusCode(500, "Error al actualizar enfermdad. -> " + ex);
            }

        }


        [HttpDelete("{id}")]
        public IActionResult DeleteEnfermedad(int id)
        {
            try
            {

            var existingEnfermedad = _context.enfermedad.FirstOrDefault(d => d.Id_Enfermedad == id);

            if (existingEnfermedad == null)
            {
                return NotFound();
            }

            _context.Database.ExecuteSqlInterpolated($"SP_EliminarEnfermedad {id}");

            return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al eliminar enfermadad. -> " + ex);
            }
        }
    }
}
