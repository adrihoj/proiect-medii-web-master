using System.ComponentModel.DataAnnotations;

namespace Proiect.Models
{
    public class Programare
    {
        public int ID { get; set; }
        public int? EnoriasID { get; set; }
        public Enorias? Enorias { get; set; }

        public int? PreotID { get; set; }

        public Preot? Preot { get; set; }
        public int? ServiciuID { get; set; }
        public Serviciu? Serviciu { get; set; }

        public int? BisericaID { get; set; }

        public Biserica? Biserica { get; set; }


        [Display(Name = "Data Programare")]

        [DataType(DataType.Date)]

        public DateTime DataProgramare { get; set; }
    }
}
