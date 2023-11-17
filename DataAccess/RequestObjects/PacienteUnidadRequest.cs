using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.RequestObjects
{
    public class PacienteUnidadRequest
    {
        public int id_paciente_unidad { get; set; }
        public int id_paciente { get; set; }
        public int NumSeguro { get; set; }
        public string nombre_Paciente { get; set; }
        public int id_unidad { get; set; }
        public string nombre_Unidad { get; set; }
        public DateTime fecha_ingreso { get; set;}
        public DateTime fecha_salida { get; set;}
    }
}
