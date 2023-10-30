using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class DoctorDto
    {
        //public int ID_Doctor { get; set; }
        public int Codigo { get; set; }
        public string NombreD { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public int ID_Especialidad { get; set; }
    }
}
