using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Registracija Generic Repository-a
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


builder.Services.AddScoped<IKorisnikRepository, KorisnikRepository>();
builder.Services.AddScoped<IDogadjajRepository, DogadjajRepository>();
builder.Services.AddScoped<IAgendaRepository, AgendaRepository>();
builder.Services.AddScoped<IZahtevRepository, ZahtevRepository>();
builder.Services.AddScoped<IClanRepository, ClanRepository>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<INotifikacijaRepository, NotifikacijaRepository>();
builder.Services.AddScoped<IPredavacRepository, PredavacRepository>();
builder.Services.AddScoped<IParticipantRepository, ParticipantRepository>();
builder.Services.AddScoped<IKoordinatorRepository, KoordinatorRepository>();
builder.Services.AddScoped<IDeoRepository, DeoRepository>();
builder.Services.AddScoped<ISesijaRepository, SesijaRepository>();
builder.Services.AddScoped<IVremeDostupnostiRepository, VremeDostupnostiRepository>();


builder.Services.AddScoped<IKorisnikService, KorisnikService>();
builder.Services.AddScoped<IDogadjajService, DogadjajService>();
builder.Services.AddScoped<IAgendaService, AgendaService>();
builder.Services.AddScoped<IZahtevService, ZahtevService>();
builder.Services.AddScoped<IVremeDostupnostiService, VremeDostupnostiService>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<INotifikacijaService, NotifikacijaService>();
builder.Services.AddScoped<IClanService, ClanService>();
builder.Services.AddScoped<IPredavacService, PredavacService>();
builder.Services.AddScoped<IParticipantService, ParticipantService>();
builder.Services.AddScoped<ISesijaService, SesijaService>();
builder.Services.AddScoped<IAutentifikacijaService, AutentifikacijaService>();
builder.Services.AddScoped<IKoordinatorService, KoordinatorService>();
builder.Services.AddSignalR();
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "TvojBackend", 
        ValidAudience = "TvojFrontend",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Ovo_Mora_Biti_Jako_Dugacak_Kljuc_Od_Barem_32_Karaktera!"))
    };
});
// JSON opcija za sprečavanje beskonačne petlje (Circular Reference) zbog navigacionih propertija
builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
builder.Services.AddCors(options =>
{
    options.AddPolicy("CORS", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .WithOrigins("http://localhost:5500",
                           "https://localhost:5500",
                           "http://127.0.0.1:5500",
                           "https://127.0.0.1:5500");
    });
});

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CORS");

app.UseHttpsRedirection();
app.UseAuthentication(); // Prvo ko si
app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/chatHub");
app.Run();
