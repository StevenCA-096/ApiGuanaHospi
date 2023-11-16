using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.RequestViews
{
    public class TopIntervencionesPorDoctor
    {
        public string DOCTOR { get; set; }
        public int CÓDIGO { get; set; }
        public string ESPECIALIDAD { get; set; }
        public int INTERVENCIONES { get; set; }

    }
}
