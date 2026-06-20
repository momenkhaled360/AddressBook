using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressBook.BLL.DTOs;
using AddressBook.BLL.Exceptions;
using AddressBook.BLL.Interfaces;
using AddressBook.DAL.Entities;
using AddressBook.DAL.Repositories.Interfaces;
using AutoMapper;

namespace AddressBook.BLL.Services
{
    public class JobService : IJobService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public JobService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        private IRepository<Job> JobRepo => _unitOfWork.GetRepository<Job>();

        public async Task<List<JobDto>> GetAllAsync()
        {
            var jobs = await JobRepo.GetAllAsync();
            return _mapper.Map<List<JobDto>>(jobs);
        }

        public async Task<JobDto?> GetByIdAsync(int id)
        {
            var job = await JobRepo.GetByIdAsync(id);
            if (job is null)
                throw new NotFoundException($"Job with id {id} was not found.");
            return _mapper.Map<JobDto>(job);
        }

        public async Task AddAsync(JobDto dto)
        {
            var job = _mapper.Map<Job>(dto);
            JobRepo.Add(job);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, JobDto dto)
        {
            var job = await JobRepo.GetByIdAsync(id);
            if (job is null)
                throw new NotFoundException($"Job with id {id} was not found.");

            _mapper.Map(dto, job);
            JobRepo.Update(job);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var job = await JobRepo.GetByIdAsync(id);
            if (job is null)
                throw new NotFoundException($"Job with id {id} was not found.");

            JobRepo.Delete(job);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}