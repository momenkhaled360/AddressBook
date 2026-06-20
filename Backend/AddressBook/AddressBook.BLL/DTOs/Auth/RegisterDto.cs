using System.ComponentModel.DataAnnotations;

namespace AddressBook.BLL.DTOs.Auth
{
    public class RegisterDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        public string Password { get; set; } = string.Empty;
    }
}