using DataAccess.Context;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiGuanaHospi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly GuanaHospiContext _context;
        public DoctorController(GuanaHospiContext context) {
            _context = context;
        }
        // GET: api/<DoctorController>
        [HttpGet]
        public List<Doctor> Get()
        {
            return _context.doctor.FromSqlRaw($"SP_ObtenerDoctores").ToList();
        }

        // GET api/<DoctorController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DoctorController>
        [HttpPost]
        public void Post([FromBody] Doctor doctor)
        {
            _context.Database.ExecuteSql($"SP_InsertarDoctor {doctor.Codigo},{doctor.Nombre},{doctor.Apellido1},{doctor.Apellido2},{doctor.ID_Especialidad}");
        }

        // PUT api/<DoctorController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DoctorController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
