 public class Notifikacija
{[Key]
    public int Id { get; set; }
    public Korisnik? Primalac{get;set;} //korisnik kome stize notifikacija

    public required string Title { get; set; }

    public required TipNotifikacije Tip{ get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Read { get; set; }

}