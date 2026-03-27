
public static class Mapper
{



    public static SesijaDTO MapToDto(Sesija s) => new()
    {
        ImeSesije = s.ImeSesije,
        VremePocetka = s.VremePocetka,
        VremeKraja = s.VremeKraja
        // Ako SesijaDTO ima polje ImePredavaca, ovde bi dodao:
        // ImePredavaca = s.Predavac != null ? s.Predavac.Ime : "Nepoznato"
    };
    public static Dogadjaj Map(DogadjajDTO d) => new()
    {
        Ime = d.Ime,
        Opis = d.Opis,
        DatumOd = d.DatumOd,
        DatumDo = d.DatumDo,
        ImageUrl = d.ImageUrl
    };

    public static Zahtev Map(ZahtevDTO d) => new()
    {
        Opis = d.Opis,
        StatusZahteva = d.Status
    };

public static Koordinator Map(KoordinatorDTO d) => new()
{
    
    Username = d.Username,
    Password = d.Password,
    Role = d.Role,
    Ime = d.Ime,
    Prezime = d.Prezime,
    Mejl = d.Mejl,
    BrojTelefona = d.BrojTelefona,
    ImageUrl = d.ImageUrl,

  
    Tip = d.Tip 
};
    public static Korisnik Map(KorisnikDTO d) => new()
    {
        Username = d.Username,
        Password = d.Password,
        Role = d.Role,
        Ime = d.Ime,
        Prezime = d.Prezime,
        Mejl = d.Mejl,
        BrojTelefona = d.BrojTelefona,
        ImageUrl = d.ImageUrl
    };

    public static Participant Map(ParticipantDTO d) => new()
    {
       
        Username = d.Username,
        Password = d.Password,
        Role = d.Role,
        Ime = d.Ime,
        Prezime = d.Prezime,
        Mejl = d.Mejl,
        BrojTelefona = d.BrojTelefona,
        Komitet = d.Komitet,
        Ishrana = d.Ishrana,
        Alerigije = d.Alerigije,
        VremeDolaska = d.VremeDolaska,
        VremeOdlaska = d.VremeOdlaska
    };

    public static Predavac Map(PredavacDTO d) => new()
    {
        Username = d.Username,
        Password = d.Password,
        Role = d.Role,
        Ime = d.Ime,
        Prezime = d.Prezime,
        Mejl = d.Mejl,
        BrojTelefona = d.BrojTelefona,
        Komitet = d.Komitet,
        Ishrana = d.Ishrana, 
        Alerigije = d.Alerigije,
        VremeDolaska = d.VremeDolaska,
        VremeOdlaska = d.VremeOdlaska
    };

    public static Sesija Map(SesijaDTO d) => new()
    {
        ImeSesije = d.ImeSesije,
        VremePocetka = d.VremePocetka,
        VremeKraja = d.VremeKraja,
      
    };
    
   
}
