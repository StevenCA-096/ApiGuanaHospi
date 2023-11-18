using DataAccess.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiGuanaHospi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class HistorialAccionesController : Controller
    {
        private readonly GuanaHospiContext _context;
        public HistorialAccionesController(GuanaHospiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetHistorialAcciones()
        {
            try
            {
                var historial = _context.HistorialAcciones
                    .FromSqlInterpolated($"select * from dbo.HistorialAcciones;")
                    .ToList();

                return Ok(historial);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno -> " + ex.Message);
            }
        }

    }
}
