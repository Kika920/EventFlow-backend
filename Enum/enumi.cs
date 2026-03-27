namespace WebTemplate.Enum
{
    public enum Status
    {
        Slobodan,
        Zauzet,
        Nedostupan
    }
public enum TipNotifikacije
    {
        StigaoZahtev,
        PromenaAgende,
        NovaPoruka
    }
    public enum StatusZahteva
    {
       prihvaceno,
       odbijeno,
       izvrseno,
       cekanje
    }

    public enum Ishrana
    {
        Vegan,
        Vegetarijanac,
        Mesojed
    }
       public enum Role
{
    Clan,
    Koordinator,
    Participant,
    Predavac
}
public enum TipKoordinatora
{
    FR,
    HR,
    CP,
    PR,
    Logistika,
    IT,
    Chairperson,//glavni za kreiranje dogadjaja
 
}
  public enum TipPodeoka
    {budjenje,
    dorucak,
     rucak,
     vecera,
      pauza,
      sesija, // ako je tip sesija onda tu predavaci mogu da upisu ime svoje sesije 
      druzenje
    }
}