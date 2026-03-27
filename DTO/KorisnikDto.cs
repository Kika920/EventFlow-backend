namespace WebTemplate.DTO
{
public class KorisnikDTO
{
[RegularExpression(@"^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$")]
    public required string Username { get; set; }
[MinLength(8)]
    public  string? Password { get; set; } //idk da li bi trebalo da imam password ovde

    public required  Role Role { get; set; }
   
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
