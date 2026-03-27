namespace WebTemplate.Models
{
    public class Zahtev
    {
[Key]
    public int Id { get; set; }

    public required string Opis { get; set; }

    public Korisnik? Posiljalac{get;set;}//ko salje zahtev

    public List<Clan>? ZaduzeniClan{ get; set; } = new List<Clan>();//kome je dodeljen zahtev vise clanova moye dobiti zahtev

    public Koordinator? Koordinator{get; set;}//ako zelim da znam koji koordinaotr je dodelio zahtev clanu
    public StatusZahteva? StatusZahteva { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? CompletedAt { get; set; }
    public Dogadjaj? Dogadjaj{get;set;}
   
}
}