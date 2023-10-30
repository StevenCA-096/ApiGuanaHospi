using DataAccess.Context;
using DataAccess.DTO;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiGuanaHospi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly GuanaHospiContext _context;
        public RolController(GuanaHospiContext context)
        {
            _context = context;
        }


        [HttpPost]
        public Rol Post([FromBody] Rol rol)
        {
            var res = _context.Database.ExecuteSql($"SP_InsertarRol {rol.NombreR}");
            return rol;
        }
    }
}
