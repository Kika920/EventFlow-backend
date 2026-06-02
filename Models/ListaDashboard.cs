using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebTemplate.Models
{
    public class ListaDashboard
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Tekst { get; set; } = string.Empty;

        public bool Zavrsen { get; set; } = false;

        public DateTime DatumKreiranja { get; set; } = DateTime.Now;

        [Required]
        public int KorisnikId { get; set; }

        [ForeignKey(nameof(KorisnikId))]
        public virtual Korisnik? Korisnik { get; set; }
    }
}