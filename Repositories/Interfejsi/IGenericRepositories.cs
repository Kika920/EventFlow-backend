using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebTemplate.Repositories
{
    public interface IGenericRepository<T> where T : class
    {DbSet<T> DbSet { get; }
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<int> SaveChangesAsync();
    }
}