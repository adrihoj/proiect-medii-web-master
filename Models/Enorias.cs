using System.ComponentModel.DataAnnotations;

namespace Proiect.Models
{
    public class Enorias
    {
        public int ID { get; set; }
        public string? Prenume { get; set; }
        public string? Nume { get; set; }

        public string Localitate { get; set; }
        public string? Adresa { get; set; }
        public string Email { get; set; }
        public string? Telefon { get; set; }
        [Display(Name = "Nume complet")]
        public string? FullName
        {
            get
            {
                return Prenume + " " + Nume;
            }
        }
        public ICollection<Programare>? Programari{ get; set; }
    }
}
