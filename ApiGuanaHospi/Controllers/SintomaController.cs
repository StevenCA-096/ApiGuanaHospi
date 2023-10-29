﻿using DataAccess.Context;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiGuanaHospi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SintomaController : ControllerBase
    {
        private readonly GuanaHospiContext _context;

        public SintomaController(GuanaHospiContext context) { 
            _context = context;
        }
        // GET: api/<SintomaController>
        [HttpGet]
        public List<Sintoma> Get()
        {
            return _context.sintoma.FromSqlInterpolated($"SP_ObtenerSintomas").ToList();
        }

        // GET api/<SintomaController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SintomaController>
        [HttpPost]
        public void Post([FromBody] Sintoma sintoma)
        {
            
            _context.Database.ExecuteSqlRaw($"SP_InsertarSintoma {sintoma.Nombre.TrimEnd()}");
        }

        // PUT api/<SintomaController>/5
        [HttpPut]
        public void Put([FromBody] Sintoma sintoma)
        {
            _context.Database.ExecuteSqlRaw($"SP_ActualizarSintoma {sintoma.ID_Sintoma},{sintoma.Nombre}");
        }

        // DELETE api/<SintomaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _context.Database.ExecuteSqlRaw($"SP_EliminarSintoma {id}");
        }
    }
}
