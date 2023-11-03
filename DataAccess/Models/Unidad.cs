using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Unidad
    {
        public int ID_Unidad { get; set; }
        public int CodigoUnidad { get; set; }
        public string Nombre { get; set;}
        public int Planta { get; set; }
        public int FID_Doctor { get; set; }

        public Doctor Doctor { get; set; }
    }
}
