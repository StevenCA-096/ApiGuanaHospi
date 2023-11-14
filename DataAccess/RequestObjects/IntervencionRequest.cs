using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.RequestObjects
{
    public class IntervencionRequest
    {
        public int ID_Intervencion { get; set; }
        public DateTime Fecha_Intervencion { get; set; }
        public string prescripcion { get; set; }
        public string NombreTi { get; set; }
        public string NombreE { get; set; }
        public string NombreP { get; set; }
        public string NombreD { get; set; }

    }
}
