using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class TipoIntervencion
    {
        public int iD_TipoIntervencion { get; set; }
        public string Nombre{ get; set; }
        public List<Intervencion>? intervenciones { get; set; }
    }
}
