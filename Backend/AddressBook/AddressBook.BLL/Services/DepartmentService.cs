using AddressBook.BLL.DTOs;
using AddressBook.BLL.Exceptions;
using AddressBook.BLL.Interfaces;
using AddressBook.DAL.Entities;
using AddressBook.DAL.Repositories.Interfaces;
using AutoMapper;

namespace AddressBook.BLL.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        private IRepository<Department> DepartmentRepo => _unitOfWork.GetRepository<Department>();

        public async Task<List<DepartmentDto>> GetAllAsync()
        {
            var departments = await DepartmentRepo.GetAllAsync();
            return _mapper.Map<List<DepartmentDto>>(departments);
        }

        public async Task<DepartmentDto?> GetByIdAsync(int id)
        {
            var department = await DepartmentRepo.GetByIdAsync(id);
            if (department is null)
                throw new NotFoundException($"Department with id {id} was not found.");
            return _mapper.Map<DepartmentDto>(department);
        }

        public async Task AddAsync(DepartmentDto dto)
        {
            var department = _mapper.Map<Department>(dto);
            DepartmentRepo.Add(department);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, DepartmentDto dto)
        {
            var department = await DepartmentRepo.GetByIdAsync(id);
            if (department is null)
                throw new NotFoundException($"Department with id {id} was not found.");

            department.Name = dto.Name;
            DepartmentRepo.Update(department);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var department = await DepartmentRepo.GetByIdAsync(id);
            if (department is null)
                throw new NotFoundException($"Department with id {id} was not found.");

            DepartmentRepo.Delete(department);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}