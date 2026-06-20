using AddressBook.BLL.DTOs;
using AddressBook.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.BLL.Interfaces
{
    public interface IContactService
    {
        Task<List<ContactDto>> GetAllAsync();

        Task<ContactDto?> GetByIdAsync(int id);

        Task AddAsync(CreateContactDto dto);

        Task UpdateAsync(int id, UpdateContactDto dto);

        Task DeleteAsync(int id);

        Task<PagedResult<ContactDto>> SearchAsync(ContactSearchDto search);

        Task<byte[]> ExportToExcelAsync();
    }
}
