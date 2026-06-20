using AddressBook.BLL.DTOs;
using AddressBook.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.BLL.Interfaces
{
    public interface IJobService
    {
        Task<List<JobDto>> GetAllAsync();

        Task<JobDto?> GetByIdAsync(int id);

        Task AddAsync(JobDto job);

        Task UpdateAsync(int id, JobDto dto);

        Task DeleteAsync(int id);
    }
}
