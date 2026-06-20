using AddressBook.BLL.DTOs;
using AddressBook.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.API.Controllers
{
    [Authorize]
    public class JobController : BaseController
    {
        private readonly IJobService _jobService;

        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var jobs = await _jobService.GetAllAsync();
            return Ok(jobs);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var job = await _jobService.GetByIdAsync(id);
            return Ok(job);
        }

        [HttpPost]
        public async Task<IActionResult> Create(JobDto dto)
        {
            await _jobService.AddAsync(dto);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            JobDto dto)
        {
            await _jobService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _jobService.DeleteAsync(id);
            return NoContent();
        }
    }
}