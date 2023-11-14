using DataAccess.Context;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiGuanaHospi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoIntervencionController : ControllerBase
    {
        private readonly GuanaHospiContext _context;
        public TipoIntervencionController(GuanaHospiContext context) {
            _context = context;
        }


        [HttpGet]
        public List<TipoIntervencion> Get()
        {
            return _context.tipoIntervencion.FromSqlInterpolated($"SP_ObtenerTipoIntervencion").ToList();
        }

        // GET api/<TipoIntervencionController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TipoIntervencionController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TipoIntervencionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TipoIntervencionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
