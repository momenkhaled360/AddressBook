public class ContactDto
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public int JobId { get; set; }

    public string JobName { get; set; } = null!;

    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public string MobileNumber { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public int Age { get; set; }

    public string Address { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Photo { get; set; }
}