namespace WebTemplate.DTO
{
  
  public class GostDTO
{
    public int Id { get; set; }
    public string Ime { get; set; } = string.Empty;
    public string Prezime { get; set; } = string.Empty;
    public string Komitet { get; set; } = string.Empty;

    public Role Role { get; set; }   // <-- BITNO

    public Ishrana? Ishrana { get; set; }
    public string? Alergije { get; set; }

    public DateTime? VremeDolaska { get; set; }
    public DateTime? VremeOdlaska { get; set; }
}

}