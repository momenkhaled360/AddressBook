using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.BLL.Interfaces
{
    public interface IFileService
    {
        Task<string?> Upload(string? folderName, IFormFile? file);
        bool Delete(string fileName, string folderName);
    }
}
