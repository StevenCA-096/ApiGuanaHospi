using DataAccess.Context;
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
                var sintomaEnfermedad = _context.paciente_unidad
                    .FromSqlInterpolated($"EXEC SP_ObtenerPacienteUnidad")
                    .ToList();

                return Ok(sintomaEnfermedad);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno. -> " + ex.Message);
            }
        }
    }
}
