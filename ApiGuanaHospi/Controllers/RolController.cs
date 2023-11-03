using DataAccess.Context;
using DataAccess.DTO;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiGuanaHospi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly GuanaHospiContext _context;
        public RolController(GuanaHospiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Rol> GetAllRol()
        {
            var roles = _context.rol
                .FromSqlInterpolated($"EXEC SP_ObtenerRoles")
                .ToList();



            return roles;
        }

        [HttpGet("{id}")]
        public IActionResult GetRolById(int id)
        {
            var rol = _context.rol
        .FromSqlInterpolated($"EXEC SP_ObtenerRolPorID {id}")
        .AsEnumerable()
        .SingleOrDefault();

            if (rol == null)
            {
                return NotFound();
            }

            return Ok(rol);
        }

        [HttpPost]
        public IActionResult CrearRol(RolDto rolDTO)
        {

            var rol = new Rol
            {
                Nombre = rolDTO.NombreR
            };

            _context.Database.ExecuteSqlInterpolated($"SP_InsertarRol {rol.Nombre}");

            return Ok("Rol creado exitosamente");
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarRol(int id, [FromBody] RolUpdateDTO RolDTO)
        {
            var existingRol = _context.rol.FirstOrDefault(r => r.Id_Rol == id);

            if (existingRol == null)
            {
                return NotFound();
            }

            existingRol.Nombre = RolDTO.NombreR;

            _context.Database.ExecuteSqlInterpolated($"SP_ActualizarRol {id}, {existingRol.Nombre}");

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarRol(int id)
        {

            var existingRol = _context.rol.FirstOrDefault(r => r.Id_Rol == id);

            if (existingRol == null)
            {
                return NotFound();
            }

            _context.Database.ExecuteSqlInterpolated($"SP_EliminarRol {id}");

            return NoContent();
        }
    }
}
