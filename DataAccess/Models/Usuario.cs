using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Usuario
    {
        public int Id_Usuario { get; set; }
        public string Correo { get; set; }
        public string Contra { get; set; }
        public int ID_Rol { get; set;}

        public Rol rol { get; set; }
    }
}
