namespace AddressBook.BLL.DTOs
{
    public class ContactSearchDto
    {
        public string? Keyword { get; set; }

        public string? MobileNumber { get; set; }

        public int? JobId { get; set; }

        public int? DepartmentId { get; set; }

        public DateTime? DateOfBirthFrom { get; set; }

        public DateTime? DateOfBirthTo { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}