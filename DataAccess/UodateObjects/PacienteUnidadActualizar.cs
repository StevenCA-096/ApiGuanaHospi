using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UodateObjects
{
    public class PacienteUnidadActualizar
    {
        public int Id_Paciente_Unidad { get; set; }
        public int Id_Paciente { get; set; }
        public int Id_Unidad { get; set; }
        public DateTime fecha_ingreso { get; set; }
        public DateTime fecha_salida { get; set; }
    }
}
