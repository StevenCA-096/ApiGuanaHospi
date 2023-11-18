using DataAccess.Context;
using DataAccess.DTO;
using DataAccess.Models;
using DataAccess.RequestObjects;
using DataAccess.UodateObjects;
using Microsoft.AspNetCore.Authorization;
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
        public List<UnidadRequest> GetAllUnidades()
        {
                var unidades = _context.unidadRequests.FromSqlInterpolated($"SP_ObtenerUnidad").ToList();

            

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
        [Authorize(Roles = "Gestion,Admin")]
        public IActionResult PostUnidad(int idUsuario,[FromBody] UnidadDTO unidadDTO)
        {
            _context.Database.OpenConnection();
            _context.Database.ExecuteSqlRaw($"EXEC sp_set_session_context 'user_id', {idUsuario};");
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
            _context.Database.CloseConnection();
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


        [HttpPut]
        [Authorize(Roles = "Gestion,Admin")]
        public IActionResult PutUnidad(int idUsuario, [FromBody] UnidadActualizar unidad)
        {
            _context.Database.OpenConnection();
            _context.Database.ExecuteSqlRaw($"EXEC sp_set_session_context 'user_id', {idUsuario};");
            try
            {
                var result = _context.Database.ExecuteSqlInterpolated($"SP_ActualizarUnidad {unidad.ID_Unidad}, {unidad.Codigo},{unidad.Nombre},{unidad.Planta},{unidad.ID_Dcotor}");
                _context.Database.CloseConnection();
                if (result == 2)
                {
                    return Ok("Unidad actualizada");
                }
                else {
                    return StatusCode(404);
                }
                

            }
            catch (Exception ex){
                return StatusCode(404);
            }
            
        }

        [HttpDelete]
        [Authorize(Roles = "Gestion,Admin")]
        public IActionResult DeleteUnidad(int id,int idUsuario)
        {
            _context.Database.OpenConnection();
            _context.Database.ExecuteSqlRaw($"EXEC sp_set_session_context 'user_id', {idUsuario};");
            var existingUnidad = _context.unidad.FirstOrDefault(d => d.ID_Unidad == id);

            if (existingUnidad == null)
            {
                _context.Database.CloseConnection();
                return NotFound();
            }

            _context.Database.ExecuteSqlInterpolated($"SP_EliminarUnidad {id}");
            _context.Database.CloseConnection();
            return NoContent();
        }
    }
}
