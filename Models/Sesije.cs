namespace WebTemplate.Models
{
    public class Sesija
    {
     [Key]
    public int Id { get; set; }
    [MaxLength(30)]
    public required string ImeSesije { get; set; }
    public TimeSpan? VremePocetka{get; set;}
    public TimeSpan? VremeKraja{get;set;}
    public Predavac? Predavac{get;set;}
    public Deo? Deo{get;set;}

    }
}