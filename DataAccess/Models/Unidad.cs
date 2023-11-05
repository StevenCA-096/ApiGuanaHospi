using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Unidad
    {
        public int ID_Unidad { get; set; }
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public int Planta { get; set; }
        public int Id_Doctor { get; set; }

        [JsonIgnore]
        public Doctor Doctor { get; set; }
    }
}
