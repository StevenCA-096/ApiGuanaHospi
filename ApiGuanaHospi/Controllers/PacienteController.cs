using DataAccess.Context;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiGuanaHospi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : Controller
    {
        private readonly GuanaHospiContext _context;
        public PacienteController(GuanaHospiContext context) {
            _context = context;
        }

        [HttpGet]
        public List<Paciente> get() { 
            return _context.paciente.FromSqlInterpolated($"SP_ObtenerPacientes").ToList();
        }
    }
}
