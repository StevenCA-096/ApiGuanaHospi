using DataAccess.Context;
using DataAccess.DTO;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Text;

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

        [HttpGet]
        public List<Doctor> GetAllDoctor()
        {
            var doctores = _context.doctor
                .FromSqlInterpolated($"EXEC SP_ObtenerDoctores")
                .ToList();

            foreach (var doctor in doctores)
            {
                _context.Entry(doctor)
                    .Reference(d => d.especialidad)
                    .Load();

                _context.Entry(doctor)
                    .Collection(d => d.unidad)
                    .Load();
            }

            return doctores;
        }

        [HttpGet("{id}")]
        public IActionResult GetDoctorById(int id)
        {
            var doctor = _context.doctor
            .FromSqlInterpolated($"EXEC SP_ObtenerDoctorPorId {id}")
            .AsEnumerable()
            .SingleOrDefault();

            if (doctor == null)
            {
                return NotFound();
            }
             

            if (doctor != null)
            {
                _context.Entry(doctor)
                    .Reference(d => d.especialidad)
                    .Load();

                _context.Entry(doctor)
                    .Collection(d => d.unidad)
                    .Load();
            }

            return Ok(doctor);
        }

        [HttpPost]
        public IActionResult CrearDoctor(int id,DoctorDto doctorDTO)
        {
            _context.Database.OpenConnection();

            _context.Database.ExecuteSqlRaw($"exec SP_Contexto ${id}");
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

            _context.Database.CloseConnection();
            return Ok("Doctor creado exitosamente");
        }

        [HttpPut("{id}")]
            public IActionResult ActualizarDoctor(int id, [FromBody] DoctorDto doctorDTO)
            {
                var existingDoctor = _context.doctor.FirstOrDefault(d => d.ID_Doctor == id);

                if (existingDoctor == null)
                {
                    return NotFound();
                }

                existingDoctor.Codigo = doctorDTO.Codigo;
                existingDoctor.NombreD = doctorDTO.NombreD;
                existingDoctor.Apellido1 = doctorDTO.Apellido1;
                existingDoctor.Apellido2 = doctorDTO.Apellido2;
                existingDoctor.ID_Especialidad = doctorDTO.ID_Especialidad;

                _context.Database.ExecuteSqlInterpolated($"SP_ActualizarDoctor {id}, {existingDoctor.Codigo},{existingDoctor.NombreD},{existingDoctor.Apellido1},{existingDoctor.Apellido2},{existingDoctor.ID_Especialidad}");

                _context.SaveChanges();

                return NoContent();
            }

            [HttpDelete("{id}")]
            public IActionResult EliminarDoctor(int id)
            {
                // Verifica si existe el doctor con el ID proporcionado
                var existingDoctor = _context.doctor.FirstOrDefault(d => d.ID_Doctor == id);

                if (existingDoctor == null)
                {
                    // No encontrado
                    return NotFound();
                }

                // Ejecuta el Stored Procedure para eliminar el doctor
                _context.Database.ExecuteSqlInterpolated($"SP_EliminarDoctor {id}");

                return NoContent();
            }

    }
}
