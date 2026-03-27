namespace WebTemplate.Models
{

    public class Deo
    {
        [Key]
        public int Id { get; set; }

        public TipPodeoka? Tip { get; set; }
      public Agenda? Agenda{get;set;}
   
        public List<Sesija>? Sesije{get;set;}
    public required DateTime Datum { get; set; }
    public required TimeSpan VremeOd { get; set; }
    public required TimeSpan VremeDo { get; set; }

    }
}