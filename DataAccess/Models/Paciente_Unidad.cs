using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Paciente_Unidad
    {
        public int ID_Paciente_Unidad { get; set; }

        public int Id_Paciente { get; set; }
        [JsonIgnore]
        [ForeignKey("Id_Paciente")]
        public Paciente? paciente { get; set; }

        public int Id_Unidad { get; set; }
        [JsonIgnore]
        [ForeignKey("Id_Unidad")]
        public Unidad? unidad { get; set; }

        public DateTime Fecha_Ingreso { get; set; }
        public DateTime Fecha_Salida { get; set; }
    }
}
