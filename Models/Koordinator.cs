namespace WebTemplate.Models
{
    public class Koordinator :Korisnik
    {
        public required TipKoordinatora Tip { get; set; }
       
      public Dogadjaj? Dogadjaj { get; set; }//menjaju se koordinaotri za svaki dogadjaj
     //   public List<Clan>? Clanovi{get;set;}//clanovi kojima dodelju zadatak
      //  public Agenda? Agenda {get;set;}//agenda koju menja
        public List<Notifikacija>? Notifikacije{get;set;}//lista kopija
      //koordinator u bazi koji kreira nov dogadjaj i vidi listu dogadjaja
      
    }
}