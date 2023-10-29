using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Doctor
    {
        public int Id_Doctor { get; set; }
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public int ID_Especialidad { get; set; }
        [JsonIgnore]
        public Especialidad? especialidad { get; set; }
    }
}
