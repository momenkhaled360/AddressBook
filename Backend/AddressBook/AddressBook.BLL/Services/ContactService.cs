using AddressBook.BLL.DTOs;
using AddressBook.BLL.Exceptions;
using AddressBook.BLL.Interfaces;
using AddressBook.DAL.Entities;
using AddressBook.DAL.Repositories.Interfaces;
using AutoMapper;
using ClosedXML.Excel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.BLL.Services
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public ContactService(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
        }

        private IRepository<Contact> ContactRepo => _unitOfWork.GetRepository<Contact>();

        public async Task<List<ContactDto>> GetAllAsync()
        {
            var contacts = await _unitOfWork
                .ContactRepository
                .GetAllWithDetailsAsync();
            return _mapper.Map<List<ContactDto>>(contacts);
        }

        public async Task<ContactDto?> GetByIdAsync(int id)
        {
            var contact = await _unitOfWork
                .ContactRepository
                .GetByIdWithDetailsAsync(id);
            if (contact is null)
                throw new NotFoundException($"Contact with id {id} was not found.");
            return _mapper.Map<ContactDto>(contact);
        }

        public async Task AddAsync(CreateContactDto dto)
        {
            var emailExists = await ContactRepo.AnyAsync(
                c => c.Email == dto.Email
            );

            if (emailExists)
                throw new BadRequestException(
                    "A contact with this email already exists."
                );

            var mobileExists = await ContactRepo.AnyAsync(
                c => c.MobileNumber == dto.MobileNumber
            );

            if (mobileExists)
                throw new BadRequestException(
                    "A contact with this mobile number already exists."
                );

            var contact = _mapper.Map<Contact>(dto);

            var photoName = await _fileService.Upload(
                "contacts",
                dto.Photo
            );

            contact.Photo = photoName;

            ContactRepo.Add(contact);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch
            {
                if (!string.IsNullOrEmpty(photoName))
                {
                    _fileService.Delete(
                        photoName,
                        "contacts"
                    );
                }

                throw;
            }
        }

        public async Task UpdateAsync(
            int id,
            UpdateContactDto dto
        )
        {
            var contact = await ContactRepo.GetByIdAsync(id);

            if (contact is null)
                throw new NotFoundException(
                    $"Contact with id {id} was not found."
                );

            var emailExists = await ContactRepo.AnyAsync(
                c => c.Email == dto.Email
                  && c.Id != id
            );

            if (emailExists)
                throw new BadRequestException(
                    "A contact with this email already exists."
                );

            var mobileExists = await ContactRepo.AnyAsync(
                c => c.MobileNumber == dto.MobileNumber
                  && c.Id != id
            );

            if (mobileExists)
                throw new BadRequestException(
                    "A contact with this mobile number already exists."
                );
            if (dto.Photo is not null)
            {
                var oldPhoto = contact.Photo;

                var newPhoto = await _fileService.Upload(
                    "contacts",
                    dto.Photo
                );

                contact.Photo = newPhoto;

                if (!string.IsNullOrEmpty(oldPhoto))
                {
                    _fileService.Delete(
                        oldPhoto,
                        "contacts"
                    );
                }
            }

            _mapper.Map(dto, contact);

            ContactRepo.Update(contact);

            await _unitOfWork.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var contact = await ContactRepo.GetByIdAsync(id);

            if (contact is null)
                throw new NotFoundException(
                    $"Contact with id {id} was not found."
                );

            if (!string.IsNullOrEmpty(contact.Photo))
            {
                _fileService.Delete(
                    contact.Photo,
                    "contacts"
                );
            }

            ContactRepo.Delete(contact);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<PagedResult<ContactDto>> SearchAsync(ContactSearchDto search)
        {
            var query = _unitOfWork

                .ContactRepository

                .GetQueryableWithDetails();

            if (!string.IsNullOrWhiteSpace(search.Keyword))
            {
                query = query.Where(c =>

                    c.FullName.Contains(search.Keyword)

                    ||

                    c.Email.Contains(search.Keyword)

                    ||

                    c.MobileNumber.Contains(search.Keyword)

                );
            }

            if (search.JobId.HasValue)
            {
                query = query.Where(c =>

                    c.JobId ==
                    search.JobId);
            }

            if (search.DepartmentId.HasValue)
            {
                query = query.Where(c =>

                    c.DepartmentId ==
                    search.DepartmentId);
            }

            if (search.DateOfBirthFrom.HasValue)
            {
                query = query.Where(c =>

                    c.DateOfBirth >=
                    search.DateOfBirthFrom.Value);
            }

            if (search.DateOfBirthTo.HasValue)
            {
                query = query.Where(c =>

                    c.DateOfBirth <=
                    search.DateOfBirthTo.Value);
            }

            var totalCount =
                await query.CountAsync();

            var contacts = await query

                .Skip(
                    (search.PageNumber - 1)
                    * search.PageSize
                )

                .Take(
                    search.PageSize
                )

                .ToListAsync();

            return new PagedResult<ContactDto>
            {
                Items = _mapper.Map<List<ContactDto>>
                (
                    contacts
                ),

                TotalCount = totalCount,

                PageNumber = search.PageNumber,

                PageSize = search.PageSize
            };
        }

        public async Task<byte[]> ExportToExcelAsync()
        {
            var contacts = await _unitOfWork
                .ContactRepository
                .GetAllWithDetailsAsync();

            using var workbook = new XLWorkbook();

            var worksheet = workbook.Worksheets.Add("Contacts");

            worksheet.Cell(1, 1).Value = "Full Name";

            worksheet.Cell(1, 2).Value = "Email";

            worksheet.Cell(1, 3).Value = "Mobile";

            worksheet.Cell(1, 4).Value = "Job";

            worksheet.Cell(1, 5).Value = "Department";

            worksheet.Cell(1, 6).Value = "Date Of Birth";

            for (int i = 0; i < contacts.Count; i++)
            {
                var row = i + 2;

                worksheet.Cell(row, 1).Value =
                    contacts[i].FullName;

                worksheet.Cell(row, 2).Value =
                    contacts[i].Email;

                worksheet.Cell(row, 3).Value =
                    contacts[i].MobileNumber;

                worksheet.Cell(row, 4).Value =
                    contacts[i].Job?.Name;

                worksheet.Cell(row, 5).Value =
                    contacts[i].Department?.Name;

                worksheet.Cell(row, 6).Value =
                    contacts[i].DateOfBirth;
            }

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();

            workbook.SaveAs(stream);

            return stream.ToArray();
        }
    }
}