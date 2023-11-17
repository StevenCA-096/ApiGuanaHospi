using DataAccess.Context;
using DataAccess.RequestObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiGuanaHospi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataWarehouseController : ControllerBase
    {
        private readonly DataWarehouseContext _DWcontext;
        public DataWarehouseController(DataWarehouseContext context)
        {
            _DWcontext = context;
        }

        [HttpGet("{NombreReporte}")]
        public IActionResult GetReportById(string NombreReporte)
        {
            try
            {

                if (NombreReporte == "TopUnidadesPorPacientes")
                {
                    var report = _DWcontext.TopUnidadesPorPacientes.FromSqlInterpolated($"select * from dbo.VW_TopUnidadesPorPacientes;").ToList();
                    return Ok(report);
                }                                                                                                                           
                else if (NombreReporte == "TopEnfermedadesPorIntervenciones") {
                    var report = _DWcontext.TopEnfermedadesPorIntervenciones.FromSqlInterpolated($"select * from dbo.VW_TopEnfermedadesPorIntervenciones;").ToList();
                    return Ok(report);
                }
                else if (NombreReporte == "TopIntervencionesPorDoctor")
                {
                    var report = _DWcontext.TopIntervencionesPorDoctor.FromSqlInterpolated($"select * from dbo.VW_TopIntervencionesPorDoctor;").ToList();
                    return Ok(report);
                }
                else if (NombreReporte == "TopIntervencionesPorTipo")
                {
                    var report = _DWcontext.TopIntervencionesPorTipo.FromSqlInterpolated($"select * from dbo.VW_TopIntervencionesPorTipo;").ToList();
                    return Ok(report);
                }
                else if (NombreReporte == "TopIntervencionesPorPaciente")
                {
                    var report = _DWcontext.TopIntervencionesPorPaciente.FromSqlInterpolated($"select * from dbo.VW_TopIntervencionesPorPaciente;").ToList();
                    return Ok(report);
                }
                else if (NombreReporte == "TopSintomasPorEnfermedad")
                {
                    var report = _DWcontext.TopSintomasPorEnfermedad.FromSqlInterpolated($"select * from dbo.VW_TopSintomasPorEnfermedad;").ToList();
                    return Ok(report);
                }
                else if (NombreReporte == "TopSintomasAtendidos")
                {
                    var report = _DWcontext.TopSintomasAtendidos.FromSqlInterpolated($"select * from dbo.VW_TopSintomasAtendidos;").ToList();
                    return Ok(report);
                } else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno. -> " + ex.Message);
            }
        }
    }
}
