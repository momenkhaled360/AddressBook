using AddressBook.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.DAL.Repositories.Interfaces
{
    public interface IContactRepository : IRepository<Contact>
    {
        Task<List<Contact>> GetAllWithDetailsAsync();

        Task<Contact?> GetByIdWithDetailsAsync(int id);

        IQueryable<Contact> GetQueryableWithDetails();
    }
}
