namespace WebTemplate.Repositories{

public class SesijaRepository : GenericRepository<Sesija>, ISesijaRepository 
{ 
    public SesijaRepository(AppDbContext context) : base(context) { } 
    public async Task<Sesija?> GetSesijaSaPredavacem(int id)
    {
        return await DbSet.Include(s => s.Predavac).FirstOrDefaultAsync(s => s.Id == id);
    }
}
}