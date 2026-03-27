namespace WebTemplate.Models{
  public class Clan:Korisnik
    {
        public int? BrojIzvrsenihZahteva { get; set; }
        public List<VremeDostupnosti>? Tabela{get;set;}
        public List<Zahtev>? Zahtevi {get;set;} 
        public Status? Status{get;set;}
    }
}