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
        public int Id_Rol { get; set; }
        public string Nombre { get; set; }

        [JsonIgnore]
        public List<Usuario>? usuarios { get; set; }
    }
}
