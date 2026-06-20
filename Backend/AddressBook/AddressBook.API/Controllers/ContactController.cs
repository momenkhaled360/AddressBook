using AddressBook.BLL.DTOs;
using AddressBook.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.API.Controllers
{
    [Authorize]
    public class ContactController : BaseController
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var contacts = await _contactService.GetAllAsync();
            return Ok(contacts);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var contact = await _contactService.GetByIdAsync(id);
            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] CreateContactDto dto
        )
        {
            await _contactService.AddAsync(dto);

            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            [FromForm] UpdateContactDto dto
        )
        {
            await _contactService.UpdateAsync(
                id,
                dto
            );

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _contactService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(
            [FromQuery] ContactSearchDto dto)
        {
            var contacts = await _contactService.SearchAsync(dto);

            return Ok(contacts);
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportToExcel()
        {
            var file = await _contactService.ExportToExcelAsync();
            return File(
                file,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "AddressBook.xlsx");
        }
    }
}