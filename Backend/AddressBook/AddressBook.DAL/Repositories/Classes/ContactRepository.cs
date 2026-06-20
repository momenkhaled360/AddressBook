using AddressBook.DAL.Data;
using AddressBook.DAL.Entities;
using AddressBook.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.DAL.Repositories.Classes
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        private readonly AppDbContext _context;

        public ContactRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<List<Contact>> GetAllWithDetailsAsync()
        {
            return await _context.Contacts
                .Include(x => x.Job)
                .Include(x => x.Department)
                .ToListAsync();
        }

        public async Task<Contact?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Contacts
                .Include(x => x.Job)
                .Include(x => x.Department)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<Contact> GetQueryableWithDetails()
        {
            return _context.Contacts

                .Include(x => x.Job)

                .Include(x => x.Department)

                .AsQueryable();
        }
    }
}