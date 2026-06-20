using AddressBook.BLL.DTOs;
using AddressBook.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.BLL.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDto>> GetAllAsync();

        Task<DepartmentDto?> GetByIdAsync(int id);

        Task AddAsync(DepartmentDto dto);

        Task UpdateAsync(int id, DepartmentDto dto);

        Task DeleteAsync(int id);
    }
}
