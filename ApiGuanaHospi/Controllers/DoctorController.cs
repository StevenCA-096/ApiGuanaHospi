using DataAccess.Context;
using DataAccess.DTO;
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
        public DoctorController(GuanaHospiContext context)
        {
            _context = context;
        }

        //[HttpGet]
        //public List<Doctor> Get()
        //{
        //    var doctores = _context.doctor
                   //colocando la instruccion sql desde la funcion
        //        .FromSqlRaw("SELECT d.ID_Doctor, d.Codigo, d.NombreD, d.Apellido1, d.Apellido2, d.ID_Especialidad, e.NombreE as NombreEspecialidad FROM Doctor d INNER JOIN Especialidad e ON e.ID_Especialidad = d.ID_Especialidad")
        //        .Include(d => d.especialidad) 
        //        // evitar la carga en cascada
        //        //.AsNoTracking()
        //        .ToList();

        //    return doctores;
        //}


        //get all
        [HttpGet]
        public List<Doctor> Get()
        {
            var doctores = _context.doctor
                //llamando al sp de la db
                .FromSqlRaw("EXEC SP_ObtenerDoctores")
                //.IgnoreQueryFilters()
                .ToList();

            //foreach para poder cargar las relaciones al llamar al sp (lo mismo que utilizar .include() pero sin llamar al EXEC SP y colocando toda la instruccion sql del join)
            foreach (var doctor in doctores)
            {
                _context.Entry(doctor)
                    .Reference(d => d.especialidad)
                    .Load();
            }

            return doctores;
        }


        // GET api/<DoctorController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public IActionResult CrearDoctor(DoctorDto doctorDTO)
        {
            // objeto Doctor a partir del DTO
            var doctor = new Doctor
            {
                //ID_Doctor = doctorDTO.ID_Doctor,
                Codigo = doctorDTO.Codigo,
                NombreD = doctorDTO.NombreD,
                Apellido1 = doctorDTO.Apellido1,
                Apellido2 = doctorDTO.Apellido2,
                ID_Especialidad = doctorDTO.ID_Especialidad,
                //null para no pasar nada al objeto de la relacion 
                especialidad = null 
            };

            _context.Database.ExecuteSqlInterpolated($"SP_InsertarDoctor {doctor.Codigo},{doctor.NombreD},{doctor.Apellido1},{doctor.Apellido2},{doctor.ID_Especialidad}");

            return Ok("Doctor creado exitosamente");
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
