namespace WebTemplate.Models
{
    public class ChatMessage
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    //  [InverseProperty("PoslatePoruke")]
        public required Korisnik? Sender { get; set; }
       
      // [InverseProperty("PrimljenePoruke")]
        public required Korisnik? Reciver { get; set; }
        public Dogadjaj? Dogadjaj{get;set;}

    }
}