using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.DAL.Entities
{
    public class Contact : BaseEntity
    {

            public string FullName { get; set; } = null!;

            public string MobileNumber { get; set; } = null!;

            public DateTime DateOfBirth { get; set; }

            public string Address { get; set; } = null!;

            public string Email { get; set; } = null!;

            public string Password { get; set; } = null!;

            public string? Photo { get; set; }

            public int Age =>
                 DateTime.Today.Year - DateOfBirth.Year;

            public int JobId { get; set; }

            public Job Job { get; set; } = null!;

            public int DepartmentId { get; set; }

            public Department Department { get; set; } = null!;
    }
}
