using AddressBook.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AddressBook.DAL.Data.Configuration
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(x => x.Name)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.HasIndex(x => x.Name)
                   .IsUnique();
        }
    }
}