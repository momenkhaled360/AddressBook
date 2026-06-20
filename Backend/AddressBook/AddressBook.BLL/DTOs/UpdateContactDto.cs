using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AddressBook.BLL.DTOs
{
    public class UpdateContactDto
    {
        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Full name must be between 3 and 100 characters.")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "Job is required.")]
        public int JobId { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Mobile number is required.")]
        [RegularExpression(@"^01[0125][0-9]{8}$", ErrorMessage = "Invalid Egyptian mobile number.")]
        public string MobileNumber { get; set; } = null!;

        [Required(ErrorMessage = "Date of birth is required.")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(250, ErrorMessage = "Address must not exceed 250 characters.")]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; } = null!;

        public IFormFile? Photo { get; set; }
}
}