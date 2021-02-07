using Coelsa.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coelsa.Repositories
{
    public interface IContactRepository : IRepository<ContactModel>
    {
        Task<IEnumerable<ContactModel>> GetContactsByCompanyAsync(string company, int pageNumber, int pageSize);
    }
}