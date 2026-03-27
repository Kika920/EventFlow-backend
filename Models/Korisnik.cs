namespace WebTemplate.Models
{
public class Korisnik
{[Key]
    public int Id { get; set; }
[RegularExpression(@"^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$")]
    public required string Username { get; set; }
[MinLength(8)]
    public required string Password { get; set; }

    public required  Role Role { get; set; }

    [InverseProperty("Reciver")]
    public List<ChatMessage>? PrimljenePoruke { get; set; }

    [InverseProperty("Sender")]
    public List<ChatMessage>? PoslatePoruke { get; set; }
   
    public required string Ime { get; set; }
   [MaxLength(50)]
    public required string Prezime { get; set; }

   [RegularExpression(@"^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$")]
    public required string Mejl { get; set; }
    [MaxLength(15)]
    public required string BrojTelefona { get; set; }

    public string? ImageUrl { get; set; }
   
}
}
