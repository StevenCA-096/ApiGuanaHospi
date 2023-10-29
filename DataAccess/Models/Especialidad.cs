using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Especialidad
    {

        public int ID_Especialidad { get; set; }
        public string Nombre { get; set; }
        [JsonIgnore]
        public List<Doctor>? doctores { get; set; }  
    }
}
