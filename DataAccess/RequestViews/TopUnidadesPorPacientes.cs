using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.RequestViews
{
    public class TopUnidadesPorPacientes
    {
        public string UNIDAD { get; set; }
        public int CÓDIGO { get; set; }
        public int PLANTA { get; set; }
        public int PACIENTES { get; set; }
        public string DOCTOR { get; set; }
        public string ESPECIALIDAD { get; set; }
    }

}
