using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UodateObjects
{
    public class PacienteActualizar
    {
        public int IdPaciente { get; set; }
        public int NumSeguro { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set;}
        public string Apellido2 { get; set;}
        public int Edad { get; set; }
    }
}
