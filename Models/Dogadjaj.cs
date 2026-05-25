namespace WebTemplate.Models
{
    public class Dogadjaj
{
[Key]
    public int Id { get; set; }

    public required string Ime { get; set; }

    public string? Opis { get; set; }

    public required DateTime DatumOd { get; set; }
public required DateTime DatumDo { get; set; }
  public List<Zahtev>? Zahtevi{get;set;}
  public List<ChatMessage>? Poruke{get;set;}

    public string? ImageUrl { get; set; }

    public List<Koordinator>? Koordinatori { get; set; }

   public List<Predavac>? Predavaci { get; set; }
    public List<Participant>? Participanti { get; set; }
     public List<Clan>? Clanovi { get; set; }

    public Agenda? Agenda { get; set; }
}
}
