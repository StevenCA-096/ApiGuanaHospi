using DataAccess.Context;
using DataAccess.DTO;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiGuanaHospi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly GuanaHospiContext _context;
        public UsuarioController(GuanaHospiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Usuario> Get()
        {
            var usuarios = _context.usuario
                .FromSqlRaw("EXEC SP_ObtenerUsuarios")
                .ToList();
            foreach (var usuario in usuarios)
            {
                _context.Entry(usuario)
                    .Reference(d => d.rol)
                    .Load();
            }

            return usuarios;
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public IActionResult CrearUsuario(UsuarioDto usuarioDTO)
        {
            //objeto del DTO
            var usuario = new Usuario
            {
                Correo = usuarioDTO.Correro,
                Contra = usuarioDTO.Contra,
                ID_Rol = usuarioDTO.ID_Rol, 
                rol = null
            };

            _context.Database.ExecuteSqlInterpolated($"SP_InsertarUsuario {usuario.Correo},{usuario.Contra}, {usuario.ID_Rol}");

            return Ok("Usuario creado exitosamente");
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Usuario usuario)
        {
            _context.Database.ExecuteSql($"SP_ActualizarUsuario {usuario.Correo}, {usuario.Contra}, {usuario.ID_Rol},");
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _context.Database.ExecuteSql($"SP_EliminarUsuario {id}");
        }
    }
}
