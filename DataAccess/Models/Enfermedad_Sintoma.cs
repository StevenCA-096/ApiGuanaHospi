using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        [ForeignKey("Id_Enfermedad")]
        public Enfermedad? enfermedad { get; set; }
        public int Id_Sintoma { get; set; }
        [JsonIgnore]
        [ForeignKey("Id_Sintoma")]
        public Sintoma? sintoma { get; set; }
    }
}
