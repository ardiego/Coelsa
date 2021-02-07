using Coelsa.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coelsa.Services
{
    public interface IContactService
    {
        Task<int> DeleteContact(int id);
        Task<IEnumerable<ContactModel>> GetAllContact();
        Task<ContactModel> GetContact(int id);
        Task<IEnumerable<ContactModel>> GetContactsByCompanyAsync(string company, int pageNumber, int pageSize);
        Task<int> InsertContact(ContactModel contact);
        Task<int> UpdateContact(int id, ContactModel contact);
    }
}