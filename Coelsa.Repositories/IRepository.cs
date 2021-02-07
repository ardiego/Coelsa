using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coelsa.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task<int> InsertAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
