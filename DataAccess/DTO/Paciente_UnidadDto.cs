using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class Paciente_UnidadDto
    {
        public int Id_Paciente { get; set; }
        public int Id_Unidad { get; set; }
        public DateTime fecha_ingreso { get; set; }
        public DateTime fecha_salida { get; set; }  
    }
}
