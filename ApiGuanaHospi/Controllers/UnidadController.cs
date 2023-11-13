using DataAccess.Context;
using DataAccess.DTO;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiGuanaHospi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnidadController : ControllerBase
    {
        private readonly GuanaHospiContext _context;
        public UnidadController(GuanaHospiContext context) { 
            _context = context;
        }

        //[HttpGet]
        //public List<Unidad> GetAllUnidad()
        //{
        //    var unidades = _context.unidad.FromSqlInterpolated($"SP_ObtenerUnidad").ToList();
        //    foreach (var unidad in unidades) { 
        //        _context.Entry(unidad).Reference(u=>u.Doctor).Load();
        //    }
        //    return unidades;
        //}

        [HttpGet]
        public List<Unidad> GetAllUnidades()
        {
            var unidades = _context.unidad.FromSqlInterpolated($"SP_ObtenerUnidad").ToList();

            foreach (var unidad in unidades)
            {
                _context.Entry(unidad)
                    .Reference(u => u.Doctor)
                    .Load();

            }

            return unidades;
        }



        [HttpGet("{id}")]
        public IActionResult GetUnidadById(int id)
        {
            var unidad = _context.unidad
            .FromSqlInterpolated($"EXEC SP_ObtenerUnidadPorId {id}")
            .AsEnumerable()
            .SingleOrDefault();

            if (unidad == null)
            {
                return NotFound();
            }

            if (unidad != null)
            {
                _context.Entry(unidad).Reference(u => u.Doctor).Load();
            }

            return Ok(unidad);
        }

        [HttpPost]
        public IActionResult PostUnidad([FromBody] UnidadDTO unidadDTO)
        {
            var unidad = new Unidad
            {
                Codigo = unidadDTO.Codigo,
                Nombre = unidadDTO.Nombre,
                Planta = unidadDTO.Planta,
                Id_Doctor = unidadDTO.ID_Doctor,
                //null para no pasar nada al objeto de la relacion 
                Doctor = null
            };

            _context.Database.ExecuteSqlInterpolated($"SP_InsertarUnidad {unidad.Codigo},{unidad.Nombre},{unidad.Planta},{unidad.Id_Doctor}");

            return Ok("Unidad creada exitosamente");
        }

        //[HttpPost]
        //public IActionResult DesasignarDoctorDeUnidad(int unidadId)
        //{
        //    var unidad = _context.unidad.FirstOrDefault(u => u.ID_Unidad == unidadId);

        //    if (unidad != null)
        //    {
        //        unidad.Id_Doctor = null;

        //        _context.SaveChanges();

        //        return Ok($"Médico desasignado de la unidad {unidadId} exitosamente");
        //    }

        //    return NotFound("No se encontró la unidad");
        //}


        [HttpPut("{id}")]
        public IActionResult PutUnidad(int id, [FromBody] UnidadDTO unidadDTO)
        {
            var existingUnidad = _context.unidad.FirstOrDefault(d => d.ID_Unidad == id);

            if (existingUnidad == null)
            {
                return NotFound();
            }

            existingUnidad.Codigo = unidadDTO.Codigo;
            existingUnidad.Nombre = unidadDTO.Nombre;
            existingUnidad.Planta = unidadDTO.Planta;
            existingUnidad.Id_Doctor = unidadDTO.ID_Doctor;

            _context.Database.ExecuteSqlInterpolated($"SP_ActualizarUnidad {id}, {existingUnidad.Codigo},{existingUnidad.Nombre},{existingUnidad.Planta},{existingUnidad.Id_Doctor}");

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUnidad(int id)
        {
            var existingUnidad = _context.unidad.FirstOrDefault(d => d.ID_Unidad == id);

            if (existingUnidad == null)
            {
                return NotFound();
            }

            _context.Database.ExecuteSqlInterpolated($"SP_EliminarUnidad {id}");

            return NoContent();
        }
    }
}
