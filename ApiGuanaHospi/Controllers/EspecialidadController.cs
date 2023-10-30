using DataAccess.Context;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiGuanaHospi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadController : ControllerBase
    {
        private readonly GuanaHospiContext _context;

        public EspecialidadController(GuanaHospiContext context)
        {
            _context = context;
        }

        // GET: api/<EspecialidadController>
        [HttpGet]
        public List<Especialidad> Get()
        {
            return _context.especialidad.FromSqlRaw("ObtenerEspecialidades").ToList();
        }

        // GET api/<EspecialidadController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<EspecialidadController>
        [HttpPost]
        public Especialidad Post([FromBody] Especialidad especialidad)
        {
            var res = _context.Database.ExecuteSql($"SP_InsertarEspecialidad {especialidad.NombreE}");
            return especialidad;
        }

        // PUT api/<EspecialidadController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string nuevoNombre)
        {
            _context.Database.ExecuteSql($"SP_ActualizarEspecialidad {id},{nuevoNombre}");
        }

        // DELETE api/<EspecialidadController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _context.Database.ExecuteSql($"SP_EliminarEspecialidad {id}");
        }
    }
}
