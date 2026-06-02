namespace WebTemplate.Data

{
        public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Korisnik> Korisnici{ get; set; }

    public DbSet<Dogadjaj> Dogadjaji { get; set; }

    public DbSet<Agenda> Agende { get; set; } //gleda se kao tabela za sesije
    public DbSet<Deo> Delovi  { get; set; } 
   public DbSet<VremeDostupnosti> TabeleDostupnosti { get; set; }
    public DbSet<Sesija> Sesije{get;set;}
    public DbSet<Clan> Clanovi { get; set; }

    public DbSet<Koordinator> Koordinatori { get; set; }
     public DbSet<ChatMessage> ChatMessages { get; set; }
    public DbSet<Participant> Participanti { get; set; }
     public DbSet<Zahtev> Zahtevi{ get; set; }
    public DbSet<Predavac> Predavaci { get; set; }
   public DbSet<Notifikacija> Notifikacije { get; set; }
   public DbSet<ListaDashboard> Liste { get; set; }
protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Definisanje 1-na-1 veze
        modelBuilder.Entity<Dogadjaj>()
            .HasOne(d => d.Agenda)
            .WithOne(a => a.Dogadjaj)
            .HasForeignKey<Agenda>(a => a.Id); // KLJUČNA LINIJA: Id Agende pokazuje na Id Dogadjaja

        
    }
}

}
