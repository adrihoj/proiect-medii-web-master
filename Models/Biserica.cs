namespace Proiect.Models
{
    public class Biserica
    {
        public int ID { get; set; }


        public string Nume { get; set; }


        public string Localitate { get; set; }

        public string Strada { get; set; }

        public string Numar { get; set; }

        public ICollection<Programare>? Programari { get; set; }
    }
}
