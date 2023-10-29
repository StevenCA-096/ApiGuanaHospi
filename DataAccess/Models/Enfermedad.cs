using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Enfermedad
    {
        public int Id_Enfermedad { get; set; }
        public string Nombre { get; set; }
        [JsonIgnore]
        public List<Enfermedad_Sintoma>? enfermedad_Sintoma { get; set; }
    }
}
