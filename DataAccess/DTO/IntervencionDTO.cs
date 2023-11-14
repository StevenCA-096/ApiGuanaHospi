using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class IntervencionDto
    {
        public int ID_Intervencion { get; set; }
        public DateTime Fecha_Intervencion { get; set; }
        public string prescripcion { get; set; }
        //Relaciones
        public int Id_TipoIntervencion { get; set; }
        public int Id_Enfermedad { get; set; }
        public int Id_Paciente { get; set; }
        public int Id_Doctor { get; set; }
    }
}
