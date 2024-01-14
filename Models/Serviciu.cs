using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proiect.Models
{
    public class Serviciu
    {
        public int ID { get; set; }

        [Display(Name = "Serviciu")]
        public string NumeServiciu { get; set; }



        [Display(Name = "Durata")]

        public TimeSpan DurataServiciu { get; set; }

        [Display(Name = "Pret")]
        [Column(TypeName = "decimal(6, 2)")]
        public decimal PretServiciu { get; set; }

        public ICollection<Programare>? Programari { get; set; }




    }
}
