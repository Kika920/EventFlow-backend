namespace WebTemplate.DTO
{
    public class LoginResponseDTO
    {
        public string TokenString { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}