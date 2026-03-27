using BCrypt.Net;
using WebTemplate.DTO;
using WebTemplate.Models;
using WebTemplate.Enum;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace WebTemplate.Services
{
  
    public class AutentifikacijaService : IAutentifikacijaService
    {
        private readonly IKorisnikRepository _repo;
        private readonly IConfiguration _config;

        public AutentifikacijaService(IKorisnikRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

       public async Task<bool> RegistracijaAsync(KorisnikDTO dto)
{
    var postojeci = await _repo.GetByUsernameAsync(dto.Username);
    if (postojeci != null) return false;

    // Generišemo heš lozinke pre inicijalizacije
    string hashedBycrypt = BCrypt.Net.BCrypt.HashPassword(dto.Password);

    Korisnik noviKorisnik = dto.Role switch
    {
        Role.Clan => new Clan 
        { 
            // SVA required polja iz Korisnika moraju biti ovde!
            Username = dto.Username,
            Password = hashedBycrypt,
            Ime = dto.Ime,
            Prezime = dto.Prezime,
            Mejl = dto.Mejl,
            BrojTelefona = dto.BrojTelefona,
            Role = dto.Role,
            // Specifično polje za Clana
            BrojIzvrsenihZahteva = 0 
        },
        Role.Predavac => new Predavac 
        { 
            Username = dto.Username,
            Password = hashedBycrypt,
            Ime = dto.Ime,
            Prezime = dto.Prezime,
            Mejl = dto.Mejl,
            BrojTelefona = dto.BrojTelefona,
            Role = dto.Role,
            // Specifična polja za Predavaca
            Komitet = "General",
            VremeDolaska = DateTime.UtcNow,
            VremeOdlaska = DateTime.UtcNow.AddDays(3)
        },
        _ => new Korisnik 
        { 
            Username = dto.Username,
            Password = hashedBycrypt,
            Ime = dto.Ime,
            Prezime = dto.Prezime,
            Mejl = dto.Mejl,
            BrojTelefona = dto.BrojTelefona,
            Role = dto.Role 
        }
    };

    await _repo.AddAsync(noviKorisnik);
    return await _repo.SaveChangesAsync() > 0;
}

        public async Task<string?> LoginAsync(string username, string password)
        {
            var user = await _repo.GetByUsernameAsync(username);
            
            // Provera: Da li korisnik postoji i da li se lozinka poklapa sa hešom u bazi
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null;
            }

            return GenerisiJwtToken(user);
        }

        private string GenerisiJwtToken(Korisnik user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? "SuperTajnaSifraOd32Karaktera"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}