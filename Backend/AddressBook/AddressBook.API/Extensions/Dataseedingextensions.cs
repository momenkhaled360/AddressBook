using AddressBook.DAL.Data;
using AddressBook.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.API.Extensions
{
    public static class DataSeedingExtensions
    {
        public static async Task SeedDataAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<AppDbContext>();

                await context.Database.MigrateAsync();

                if (!context.Departments.Any())
                {
                    context.Departments.AddRange(
                        new Department
                        {
                            Name = "IT"
                        },

                        new Department
                        {
                            Name = "HR"
                        }
                    );
                }

                if (!context.Jobs.Any())
                {
                    context.Jobs.AddRange(
                        new Job
                        {
                            Name = "Software Engineer"
                        },

                        new Job
                        {
                            Name = "Accountant"
                        }
                    );
                }

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();

                logger.LogError(
                    ex,
                    "An error occurred while applying migrations."
                );
            }
        }
    }
}