namespace WebTemplate.Repositories
{
   public interface IAgendaRepository : IGenericRepository<Agenda> 
{
    Task<Agenda?> GetAgendaSaDetaljimaAsync(int id);
    Task<Agenda?> GetByDogadjajIdAsync(int dogadjajId);
}
}