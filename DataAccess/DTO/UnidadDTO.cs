using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class UnidadDTO
    {
        public int ID_Unidad { get; set; }
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public int Planta { get; set; }
        public int FID_Doctor { get; set; }
    }
}
