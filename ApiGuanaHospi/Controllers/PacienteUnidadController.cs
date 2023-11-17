using DataAccess.Context;
using DataAccess.DTO;
using DataAccess.Models;
using DataAccess.RequestObjects;
using DataAccess.UodateObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiGuanaHospi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteUnidadController : ControllerBase
    {
        private readonly GuanaHospiContext _context;
        public PacienteUnidadController(GuanaHospiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllPacienteUnidad()
        {
            try
            {
                var sintomaEnfermedad = _context.pacienteUnidadRequests
                    .FromSqlInterpolated($"EXEC SP_ObtenerPacienteUnidad")
                    .ToList();

                return Ok(sintomaEnfermedad);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno. -> " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult crearPaciente_Unidad(int idUsuario,Paciente_UnidadDto paciente_Unidad) {
            _context.Database.OpenConnection();

            _context.Database.ExecuteSqlRaw($"EXEC sp_set_session_context 'user_id', {idUsuario};");

            try
            {
                var resultado = _context.Database.ExecuteSqlInterpolated($"SP_InsertarPaciente_Unidad {paciente_Unidad.Id_Paciente},{paciente_Unidad.Id_Unidad},{paciente_Unidad.fecha_ingreso},{paciente_Unidad.fecha_salida}");
                _context.Database.CloseConnection();
                if (resultado == 2)
                {
                    return Ok("Insertado correctamente");

                }
                else {
                    return StatusCode(500, "Error");
                }
            }
            catch (Exception ex){
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult eliminarPacienteUnidad(int idUsuario, int idPacienteUnidad)
        {
            _context.Database.OpenConnection();
            _context.Database.ExecuteSqlRaw($"EXEC sp_set_session_context 'user_id', {idUsuario};");

            try
            {
                var resultado =_context.Database.ExecuteSqlInterpolated($"SP_EliminarPacienteUnidad {idPacienteUnidad}");
                _context.Database.CloseConnection();
                if (resultado == 2)
                {
                    return Ok("Eliminado");
                }
                else {
                    return StatusCode(500, "Error en la operacion");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPut]
        public IActionResult actualizarPacienteUnidad(int idUsuario,PacienteUnidadActualizar paciente_Unidad) {
            _context.Database.OpenConnection();
            _context.Database.ExecuteSqlRaw($"EXEC sp_set_session_context 'user_id', {idUsuario};");

            try
            {
                var resultado = _context.Database.ExecuteSqlInterpolated($"SP_ActualizarPacienteUnidad {paciente_Unidad.Id_Paciente_Unidad},{paciente_Unidad.Id_Paciente},{paciente_Unidad.Id_Unidad},{paciente_Unidad.fecha_ingreso},{paciente_Unidad.fecha_salida}");
                _context.Database.CloseConnection();
                if (resultado == 2)
                {
                    return Ok("Actualizado");
                }
                else
                {
                    return StatusCode(500, "Error en la operacion");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("id")]
        public IActionResult GetById(int id) {
            var result = _context.pacienteUnidadRequests.FromSqlInterpolated($"SP_ObtenerPacienteUnidadPorID {id};");
            if (result == null)
            {
                return StatusCode(400, "No encontrado");
            }
            else {
                return Ok(result);
            }
        }
    }
}
