using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.RequestObjects
{
    
    public class UnidadRequest
    {
        
        public int ID_Unidad { get; set; }
        public int CodigoU { get; set; }
        public string NombreU { get; set; }
        public int Planta { get; set; }
        public int doctor_id{ get; set; }
      
    }
}
