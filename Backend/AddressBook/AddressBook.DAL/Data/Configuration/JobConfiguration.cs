using AddressBook.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AddressBook.DAL.Data.Configuration
{
    public class JobConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.Property(x => x.Name)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.HasIndex(x => x.Name)
                   .IsUnique();
        }
    }
}