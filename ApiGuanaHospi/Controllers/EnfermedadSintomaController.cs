using DataAccess.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiGuanaHospi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnfermedadSintomaController : ControllerBase
    {
        private readonly GuanaHospiContext _context;
        public EnfermedadSintomaController(GuanaHospiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllEnfermedadSintoma()
        {
            try
            {
                var sintomaEnfermedad = _context.enfermedad_Sintoma
                    .FromSqlInterpolated($"EXEC SP_ObtenerEnfermedadSintoma")
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
