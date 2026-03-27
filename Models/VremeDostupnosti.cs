namespace WebTemplate.Models{
public class VremeDostupnosti
{[Key]
    public int Id { get; set; }
    public DateTime Datum { get; set; }
    public TimeSpan VremeOd { get; set; }
    public TimeSpan VremeDo { get; set; }
    public Status JeDostupan { get; set; }
    public Clan? Clan { get; set; }
}}
//treba da je popune clanovi, moze da se izmeni dostupnost u toku dogadjaja