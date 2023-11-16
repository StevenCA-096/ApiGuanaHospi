using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.RequestViews
{
    public class TopIntervencionesPorPaciente
    {
        public int SEGURO { get; set; }
        public string PACIENTE { get; set; }
        public int EDAD { get; set; }
        public int INTERVENCIONES { get; set; }
    }
}
