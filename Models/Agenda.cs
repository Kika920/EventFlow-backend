namespace WebTemplate.Models
{

 public class Agenda
    {
        [Key]
        public int Id { get; set; }
       [ForeignKey("Id")]
        public virtual Dogadjaj? Dogadjaj { get; set; }
[       MaxLength(30)]
        public List<Deo>? Delovi{get;set;}

       public List<Koordinator>? Koordinatori{get;set;}}}