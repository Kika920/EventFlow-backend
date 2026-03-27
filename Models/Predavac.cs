namespace WebTemplate.Models{
public class Predavac:Korisnik
{
      public required string Komitet { get; set; }
    public string? Alerigije{get;set;}
    public Ishrana? Ishrana{get;set;}

    public required DateTime VremeDolaska{get;set;}
    public required DateTime VremeOdlaska{get;set;}
     public List<Sesija>? Sesije{get;set;}
   
}
}