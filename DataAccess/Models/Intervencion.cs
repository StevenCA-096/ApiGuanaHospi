using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Intervencion
    {
        public int ID_Intervencion { get; set; }
        public DateTime Fecha_Intervencion { get; set; }
        public string prescripcion { get; set; }
        //Relaciones
        public int Id_TipoIntervencion { get; set; }
        public TipoIntervencion? tipoIntervencion { get; set; }
        public int Id_Enfermedad{ get; set; }
        public Enfermedad? enfermedad { get; set; }
        public int Id_Paciente{ get; set; }
        public Paciente? paciente { get; set; }
        public int Id_Doctor{ get; set; }
        public Doctor? doctor { get; set; }

    }
}
