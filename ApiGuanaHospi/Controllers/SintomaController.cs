using DataAccess.Context;
using DataAccess.DTO;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlTypes;

namespace ApiGuanaHospi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SintomaController : ControllerBase
    {
        private readonly GuanaHospiContext _context;

        public SintomaController(GuanaHospiContext context) { 
            _context = context;
        }

        [HttpGet]
        public List<Sintoma> GetAllSintoma()
        {
            var sintomas = _context.sintoma
                // Llamando al sp de la db usando FromSqlInterpolated
                .FromSqlInterpolated($"EXEC SP_ObtenerSintomas")
                .ToList();

            return sintomas;
        }

        [HttpGet("{id}")]
        public IActionResult GetSintomaById(int id)
        {
            var sintoma = _context.sintoma
            .FromSqlInterpolated($"EXEC SP_ObtenerSintomaPorId {id}")
            .AsEnumerable()
            .SingleOrDefault();

            if (sintoma == null)
            {
                return NotFound();
            }

            return Ok(sintoma);
        }

        [HttpPost]
        public IActionResult PostSintoma(SintomaDto sintomaDto)
        {
            var sintoma = new Sintoma
            {
                Nombre = sintomaDto.Nombre,

            };

            _context.Database.ExecuteSqlInterpolated($"SP_InsertarSintoma {sintoma.Nombre}");

            return Ok("Sintoma creado exitosamente");
        }

        [HttpPut("{id}")]
        public IActionResult PutSintoma(int id, [FromBody] SintomaDto sintomaDto)
        {
            var existingSintoma = _context.sintoma.FirstOrDefault(d => d.ID_Sintoma == id);

            if (existingSintoma == null)
            {
                return NotFound();
            }

            existingSintoma.Nombre = sintomaDto.Nombre;

            _context.Database.ExecuteSqlInterpolated($"SP_ActualizarSintoma {id}, {existingSintoma.Nombre}");

            _context.SaveChanges();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteSintoma(int id)
        {
            var existingSintoma = _context.sintoma.FirstOrDefault(d => d.ID_Sintoma == id);

            if (existingSintoma == null)
            {
                // No encontrado
                return NotFound();
            }

            _context.Database.ExecuteSqlInterpolated($"SP_EliminarSintoma {id}");

            return NoContent();
        }
    }
}
