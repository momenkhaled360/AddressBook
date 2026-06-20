using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.DAL.Entities
{
    public class Department : BaseEntity
    {
        public string Name { get; set; } = null!;

        public ICollection<Contact> Contacts { get; set; }
            = [];
    }
}
