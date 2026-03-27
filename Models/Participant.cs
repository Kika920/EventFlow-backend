using WebTemplate.Enum;

namespace WebTemplate.Models
{
    public class Participant:Korisnik
{
    public required string Komitet { get; set; }

    public string? Alerigije{get;set;}
    public Ishrana? Ishrana{get;set;}
    public required DateTime VremeDolaska{get;set;}
    public required DateTime VremeOdlaska{get;set;}
  //  public Dogadjaj? Dogadjaj { get; set; }


}
}