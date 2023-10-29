using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Enfermedad_Sintoma
    {
        public int Id_Enfermedad_Sintoma { get; set; }
        public int Id_Enfermedad { get; set; }
        [JsonIgnore]
        public Enfermedad enfermedad { get; set; }
        public int Id_Sintoma { get; set; }
        [JsonIgnore]
        public Sintoma sintoma { get; set; }
    }
}
