 namespace WebTemplate.DTO { 
   
    public class PredavacDTO:KorisnikDTO
{
    public required string Komitet { get; set; }

    public string? Alerigije{get;set;}
    public Ishrana? Ishrana{get;set;}
    public required DateTime VremeDolaska{get;set;}
    public required DateTime VremeOdlaska{get;set;}

}

}