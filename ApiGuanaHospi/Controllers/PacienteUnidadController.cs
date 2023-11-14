using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiGuanaHospi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteUnidadController : ControllerBase
    {
        // GET: api/<PacienteUnidadController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PacienteUnidadController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PacienteUnidadController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PacienteUnidadController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PacienteUnidadController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
