using DataAccess.Context;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ApiGuanaHospi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly GuanaHospiContext _context;

        public AuthenticationController(GuanaHospiContext context)
        {
            _context = context;
        }

        [HttpGet("Login")]
        public async Task<object> Login(string email, string password)
        {
            var usuario = new Usuario();
            var rol = new Rol();
            var usuarios = _context.usuario
                .FromSqlRaw("sp_Login", email, password);
            var resultado = ("sp_Login", email, password);

            // Agregar la información del usuario a la respuesta
            resultado.Result["idUsuario"] = Usuario.Id_Usuario;
            resultado.Result["idRol"] = Usuario.ID_Rol;
            resultado.Result["rolNombre"] = Rol.Nombre;

            return resultado;
        }
    }
}
