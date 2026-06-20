using AddressBook.BLL.DTOs;
using AddressBook.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.API.Controllers
{
    public class DepartmentController : BaseController
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(
            IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var departments = await _departmentService.GetAllAsync();
            return Ok(departments);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var department = await _departmentService.GetByIdAsync(id);
            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentDto dto)
        {
            await _departmentService.AddAsync(dto);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            DepartmentDto dto)
        {
            await _departmentService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _departmentService.DeleteAsync(id);
            return NoContent();
        }
    }
}