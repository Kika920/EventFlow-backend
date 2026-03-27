
namespace WebTemplate.Repositories
{
public class DeoRepository : GenericRepository<Deo>, IDeoRepository 
{ 
    public DeoRepository(AppDbContext context) : base(context) { } 
}}