using Azure.Core;
using DataAccess.Context;
using DataAccess.DTO;
using DataAccess.Models;
using DataAccess.RequestObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiGuanaHospi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntervencionController : ControllerBase
    {
        private readonly GuanaHospiContext _context;
        public IntervencionController(GuanaHospiContext context) { 
            _context = context; 
        }

        [Authorize(Roles = "Atencion,Admin")]
        [HttpGet]
        public List<IntervencionRequest> GetUnidades() { 
           
            //var intervenciones = _context.intervencion.
            //    Include(I => I.paciente).IgnoreAutoIncludes()
            //    .Include(i => i.doctor).IgnoreAutoIncludes()
            //    .Include(i=>i.tipoIntervencion).IgnoreAutoIncludes()
            //    .Include(i=>i.enfermedad)
            //    .ToList();
            var intervenciones = _context.intervencionRequests.FromSqlInterpolated($"SP_ObtenerIntervenciones").ToList();

            if (intervenciones !=null)
            {
                return intervenciones;
            }
            else{
                return null;
            }
            
        }

        [HttpPost]
        [Authorize(Roles = "Atencion,Admin")]
        public string Post(int idUsuario,IntervencionDto request) {
            _context.Database.OpenConnection();
            _context.Database.ExecuteSqlRaw($"EXEC sp_set_session_context 'user_id', {idUsuario};");
            var resultado = _context.Database.ExecuteSqlInterpolated($"SP_InsertarIntervencion {request.Fecha_Intervencion},{request.prescripcion},{request.Id_TipoIntervencion},{request.Id_Enfermedad},{request.Id_Paciente},{request.Id_Doctor}");
            _context.Database.CloseConnection();
            if (resultado == 1)
            {
                return "Creado exitosamente";
            }
            else {
                return "Error al agregar";
            }
               
        }

        [HttpPut]
        [Authorize(Roles = "Atencion,Admin")]
        public IActionResult Put(int idUsuario,IntervencionDto request)
        {
            _context.Database.OpenConnection();
            _context.Database.ExecuteSqlRaw($"EXEC sp_set_session_context 'user_id', {idUsuario};");
            try { 
            _context.Database.ExecuteSqlInterpolated($"SP_ActualizarIntervencion {request.ID_Intervencion},{request.Fecha_Intervencion},{request.prescripcion},{request.Id_TipoIntervencion},{request.Id_Enfermedad},{request.Id_Paciente},{request.Id_Doctor}");
            _context.Database.CloseConnection();
                return Ok("Actualizado");
            }catch (Exception ex)
            {
                
                return NotFound(ex);
            }



        }

        [HttpDelete]
        [Authorize(Roles = "Atencion,Admin")]
        public string Delete(int id,int idUsuario) {
            _context.Database.OpenConnection();
            _context.Database.ExecuteSqlRaw($"EXEC sp_set_session_context 'user_id', {idUsuario};");

            var resultado = _context.Database.ExecuteSqlInterpolated($"SP_EliminaIntervencion {id}");
            _context.Database.CloseConnection();
            if (resultado >= 1)
            {
                return "Eliminado";
            }
            else {
                return "Error";
            }
            
        }
    }
}
