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
        public List<Rol> Get()
        {
            return _context.rol.FromSqlRaw("SP_ObtenerRoles").ToList();
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public Rol Post([FromBody] Rol rol)
        {
            var res = _context.Database.ExecuteSql($"SP_InsertarRol {rol.Nombre}");
            return rol;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _context.Database.ExecuteSql($"SP_EliminarRol {id}");
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string nuevoNombre)
        {
            _context.Database.ExecuteSql($"SP_ActualizarRol {id},{nuevoNombre}");
        }
    }
}
