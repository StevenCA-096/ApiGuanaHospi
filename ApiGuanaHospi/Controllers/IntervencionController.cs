using Azure.Core;
using DataAccess.Context;
using DataAccess.DTO;
using DataAccess.Models;
using DataAccess.RequestObjects;
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
        public string Post(int idUsuario,IntervencionRequest request) {
            _context.Database.OpenConnection();
            _context.Database.ExecuteSqlRaw($"exec SP_Contexto ${idUsuario}");
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
        public string Put(int idUsuario,IntervencionDTO request)
        {
            _context.Database.OpenConnection();
            _context.Database.ExecuteSqlRaw($"exec SP_Contexto ${idUsuario}");
            var resultado = _context.Database.ExecuteSqlInterpolated($"SP_ActualizarIntervencion {request.ID_Intervencion},{request.Fecha_Intervencion},{request.prescripcion},{request.Id_TipoIntervencion},{request.Id_Enfermedad},{request.Id_Paciente},{request.Id_Doctor}");
            _context.Database.CloseConnection();
            if (resultado == 1)
            {
                return "Modificado";
            }
            else {
                return "Error";
            }

            
        }

        [HttpDelete]
        public string Delete(int idUsuario,int idIntervencion) {
            _context.Database.OpenConnection();
            _context.Database.ExecuteSqlRaw($"exec SP_Contexto ${idUsuario}");

            var resultado = _context.Database.ExecuteSqlInterpolated($"SP_EliminaIntervencion {idIntervencion}");
            _context.Database.CloseConnection();
            if (resultado == 1)
            {
                return "Eliminado";
            }
            else {
                return "Error";
            }
            
        }
    }
}
