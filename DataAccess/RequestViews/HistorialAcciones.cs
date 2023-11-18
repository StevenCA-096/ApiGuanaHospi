using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.RequestViews
{
    public class HistorialAcciones
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int IdUsuario { get; set; }
        public string CorreoUsuario { get; set; }
        public string Accion { get; set; }
        public int IdRegistroModificado { get; set; }
    }
}


