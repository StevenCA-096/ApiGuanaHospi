using DataAccess.Context;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiGuanaHospi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnfermedadController : ControllerBase
    {
        private readonly GuanaHospiContext _context;

        public EnfermedadController(GuanaHospiContext context) {
            _context = context;
        }
        // GET: api/<EnfermedadController>
        [HttpGet]
        public List<Enfermedad> Get()
        {
            return _context.enfermedad.FromSqlRaw($"SP_ObtenerEnfermedades").ToList();
        }

        // GET api/<EnfermedadController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<EnfermedadController>
        [HttpPost]
        public void Post([FromBody] Enfermedad enfermedad)
        {
            _context.Database.ExecuteSql($"SP_InsertarEnfermedad {enfermedad.Nombre}");
        }

        // PUT api/<EnfermedadController>/5
        [HttpPut]
        public void Put([FromBody] Enfermedad enfermedad)
        {
            _context.Database.ExecuteSql($"SP_ActualizarEnfermedad {enfermedad.Id_Enfermedad}, {enfermedad.Nombre}");
        }

        // DELETE api/<EnfermedadController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _context.Database.ExecuteSql($"SP_EliminarEnfermedad {id}");
        }
    }
}
