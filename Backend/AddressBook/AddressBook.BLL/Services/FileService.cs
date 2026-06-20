using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressBook.BLL.Interfaces;
namespace GymMangementBLL.Services.AttachmentService
{
    public class FileService : IFileService
    {
        private static readonly string[] AllowedExtensions =
    {
        ".jpg",
        ".jpeg",
        ".png"
    };

        private const long MaxFileSize = 5 * 1024 * 1024; // 5 MB

        private readonly IWebHostEnvironment _webHost;

        public FileService(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
        }

        public async Task<string?> Upload(string? folderName, IFormFile? file)
        {
            if (string.IsNullOrWhiteSpace(folderName))
                return null;

            if (file is null || file.Length == 0)
                return null;

            if (file.Length > MaxFileSize)
                return null;

            if (string.IsNullOrWhiteSpace(_webHost.WebRootPath))
                return null;

            var extension = Path.GetExtension(file.FileName)?.ToLowerInvariant();

            if (string.IsNullOrWhiteSpace(extension) ||
                !AllowedExtensions.Contains(extension))
                return null;

            var fileName = $"{Guid.NewGuid()}{extension}";

            var folderPath = Path.Combine(
                _webHost.WebRootPath,
                "images",
                folderName
            );

            Directory.CreateDirectory(folderPath);

            var filePath = Path.Combine(folderPath, fileName);

            await using var stream = new FileStream(
                filePath,
                FileMode.Create,
                FileAccess.Write
            );

            await file.CopyToAsync(stream);

            return fileName;
        }
        public bool Delete(string fileName, string folderName)
        {
            if (string.IsNullOrWhiteSpace(fileName) ||
                string.IsNullOrWhiteSpace(folderName) ||
                string.IsNullOrWhiteSpace(_webHost.WebRootPath))
            {
                return false;
            }

            var filePath = Path.Combine(
                _webHost.WebRootPath,
                "images",
                folderName,
                fileName
            );

            if (!File.Exists(filePath))
                return false;

            File.Delete(filePath);

            return true;
        }
    }
}
