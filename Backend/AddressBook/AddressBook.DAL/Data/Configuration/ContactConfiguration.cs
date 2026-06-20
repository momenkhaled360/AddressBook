using AddressBook.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.Property(x => x.FullName)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(x => x.MobileNumber)
               .HasMaxLength(20)
               .IsRequired();

        builder.HasIndex(x => x.MobileNumber)
               .IsUnique();

        builder.Property(x => x.Address)
               .HasMaxLength(250)
               .IsRequired();

        builder.Property(x => x.Email)
               .HasMaxLength(100)
               .IsRequired();

        builder.HasIndex(x => x.Email)
               .IsUnique();

        builder.Property(x => x.Photo)
               .HasMaxLength(255);

        builder.HasOne(x => x.Job)
               .WithMany(x => x.Contacts)
               .HasForeignKey(x => x.JobId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Department)
               .WithMany(x => x.Contacts)
               .HasForeignKey(x => x.DepartmentId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}