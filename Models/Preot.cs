using System.ComponentModel.DataAnnotations;

namespace Proiect.Models
{
    public class Preot
    {

        public int ID { get; set; }

        public string Nume { get; set; }
        public string Prenume { get; set; }

        [Display(Name = "Nume")]
        public string? FullName
        {
            get
            {
                return Nume + " " + Prenume;
            }
        }

        [Display(Name = "Data nasterii")]
        public DateTime DataNasterii { get; set; }

        public ICollection<Programare>? Programari { get; set; }






    }
}
