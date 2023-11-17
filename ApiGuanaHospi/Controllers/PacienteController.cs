using DataAccess.Context;
using DataAccess.DTO;
using DataAccess.Models;
using DataAccess.UodateObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiGuanaHospi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : Controller
    {
        private readonly GuanaHospiContext _context;
        public PacienteController(GuanaHospiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllPaciente()
        {
            try
            {
                var pacientes = _context.paciente
                    .FromSqlInterpolated($"EXEC SP_ObtenerPacientes")
                    .ToList();

                return Ok(pacientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno -> " + ex.Message);
            }
        }

            
        [HttpGet("{id}")]
        public IActionResult GetPacienteById(int id)
        {
            try
            {
                var paciente = _context.paciente
                    .FromSqlInterpolated($"EXEC SP_ObtenerPacientePorId {id}")
                    .AsEnumerable()
                    .SingleOrDefault();

                if (paciente == null)
                {
                    return NotFound();
                }

                return Ok(paciente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }


        [HttpPost]
        public IActionResult PostPaciente(int idUsuario,[FromBody]  PacienteDto pacienteDto)
        {
            _context.Database.OpenConnection();
            _context.Database.ExecuteSqlRaw($"EXEC sp_set_session_context 'user_id', {idUsuario};");
            try
            {

                var paciente = new Paciente
                {
                    NumSeguro = pacienteDto.NumSeguro,
                    Nombre = pacienteDto.Nombre,
                    Apellido1 = pacienteDto.Apellido1,
                    Apellido2 = pacienteDto.Apellido2,
                    Edad = pacienteDto.Edad,
                    

                };

                var result = _context.Database.ExecuteSqlInterpolated($"SP_InsertarPaciente {paciente.NumSeguro}, {paciente.Nombre}, {paciente.Apellido1}, {paciente.Apellido2}, {paciente.Edad}");
                _context.Database.CloseConnection();    
                return CreatedAtAction(nameof(GetPacienteById), new { id = paciente.ID_Paciente }, paciente);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al crear enfermedad. -> " + ex.Message);
            }

        }


        [HttpPut]
        public IActionResult PutPaciente(int idUsuario, [FromBody] PacienteActualizar paciente)
        {
            _context.Database.OpenConnection();
            _context.Database.ExecuteSqlRaw($"EXEC sp_set_session_context 'user_id', {idUsuario};");
            try
            {       
                var result = _context.Database.ExecuteSqlInterpolated($"SP_ActualizarPaciente {paciente.IdPaciente}, {paciente.NumSeguro}, {paciente.Nombre}, {paciente.Apellido1}, {paciente.Apellido2}, {paciente.Edad}");
                _context.SaveChanges();
                _context.Database.CloseConnection();
                return Ok(paciente);

            }
            catch (Exception ex)
            {
                // Manejar el error según tus necesidades
                return StatusCode(500, "Error al actualizar paciente. -> " + ex.Message);
            }

        }


        [HttpDelete("{id}")]
        public IActionResult DeletePaciente(int id,int idUsuario)
        {
            _context.Database.OpenConnection();
            _context.Database.ExecuteSqlRaw($"EXEC sp_set_session_context 'user_id', {idUsuario};");
            try
            {

                var existingPaciente = _context.paciente.FirstOrDefault(d => d.ID_Paciente == id);

                if (existingPaciente == null)
                {
                    return NotFound();
                }

                _context.Database.ExecuteSqlInterpolated($"SP_EliminarPaciente {id}");
                _context.Database.CloseConnection();
                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al eliminar paciente. -> " + ex.Message);
            }
        }
    }
}
