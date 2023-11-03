using DataAccess.Context;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        [HttpGet]
        public List<Unidad> Get()
        {
            List<Unidad> unidades = _context.unidad.FromSql($"SP_ObtenerUnidad").ToList();
            foreach (var unidad in unidades) { 
                _context.Entry(unidad).Reference(u=>u.Doctor).Load();
            }
            return unidades;
        }

        // GET api/<UnidadController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UnidadController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UnidadController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UnidadController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
