using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Rol
    {
        public int ID_rol { get; set; }
        public string NombreR { get; set; }

        [JsonIgnore]
        public List<Usuario>? usuarios { get; set; }
    }
}
