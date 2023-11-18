using DataAccess.Context;
using DataAccess.DTO;
using DataAccess.Models;
using DataAccess.UodateObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
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

        [HttpGet]
        public IActionResult GetAllDoctor()
        {
            try
            {
                var doctores = _context.doctor
                    .FromSqlInterpolated($"EXEC SP_ObtenerDoctores")
                    .ToList();

                CargarRelaciones(doctores);

                return Ok(doctores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno. -> " + ex);
            }
        }

        private void CargarRelaciones(List<Doctor> doctores)
        {
            foreach (var doctor in doctores)
            {
                _context.Entry(doctor)
                    .Reference(d => d.especialidad)
                    .Load();

                _context.Entry(doctor)
                    .Collection(d => d.unidad)
                    .Load();
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetDoctorById(int id)
        {
            try
            {
                var doctor = _context.doctor
                    .FromSqlInterpolated($"EXEC SP_ObtenerDoctorPorId {id}")
                    .AsEnumerable()
                    .SingleOrDefault();

                    if (doctor == null)
                    {
                    Console.WriteLine(doctor);
                    return NotFound();
                    }

                // Cargar especialidad y unidades
                CargarEspecialidad(doctor);
                CargarUnidades(doctor);

                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno. -> " + ex.Message);
            }
        }

        private void CargarEspecialidad(Doctor doctor)
        {
            _context.Entry(doctor)
                .Reference(d => d.especialidad)
                .Load();
        }

        private void CargarUnidades(Doctor doctor)
        {
            _context.Entry(doctor)
                .Collection(d => d.unidad)
                .Load();
        }


        [HttpPost]
        [Authorize(Roles = "Gestion,Admin")]
        public IActionResult CrearDoctor(int idUsuario, DoctorDto doctorDTO)
        {

            try
            {
                _context.Database.OpenConnection();

                _context.Database.ExecuteSqlRaw($"EXEC sp_set_session_context 'user_id', {idUsuario};");
                // objeto Doctor a partir del DTO
                var doctor = new Doctor
                {
                    Codigo = doctorDTO.Codigo,
                    NombreD = doctorDTO.Nombre.Trim(),
                    Apellido1 = doctorDTO.Apellido1.Trim(),
                    Apellido2 = doctorDTO.Apellido2.Trim(),
                    ID_Especialidad= doctorDTO.ID_Especialidad,
                    especialidad = null
                };

                _context.Database.ExecuteSqlInterpolated($"SP_InsertarDoctor {doctor.Codigo},{doctorDTO.Nombre.Trim()},{doctor.Apellido1.Trim()},{doctor.Apellido2.Trim()},{doctor.ID_Especialidad}");

                _context.Database.CloseConnection();
                return CreatedAtAction(nameof(GetDoctorById), new { id = doctor.ID_Doctor }, doctor);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al crear el doctor. -> " + ex);
            }
        }


        [HttpPut]
        [Authorize (Roles = "Gestion,Admin")]
        public IActionResult ActualizarDoctor(int idUsuario, [FromBody] DoctorActualizar doctor)
        {
            _context.Database.OpenConnection();
            
            _context.Database.ExecuteSqlRaw($"EXEC sp_set_session_context 'user_id', {idUsuario};");

            try
            {
                _context.Database.ExecuteSqlInterpolated($"SP_ActualizarDoctor {doctor.IdDoctor}, {doctor.Codigo},{doctor.Nombre.Trim()},{doctor.Apellido1.Trim()},{doctor.Apellido2.Trim()},{doctor.IdEspecialidad}");

                _context.Database.CloseConnection();
                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al actualizar el doctor. -> " + ex);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Gestion,Admin")]
        public IActionResult EliminarDoctor(int id,int idUsuario)
        {
            _context.Database.OpenConnection();
            _context.Database.ExecuteSqlRaw($"EXEC sp_set_session_context 'user_id', {idUsuario};");
           
            var existingDoctor = GetDoctorById(id);

            if (existingDoctor == null)
            {
                return NotFound();
            }

            try
            {
                
                _context.Database.ExecuteSqlInterpolated($"SP_EliminarDoctor {id}");
                _context.Database.CloseConnection();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al eliminar el doctor. -> " + ex);
            }
        }


    }
}
