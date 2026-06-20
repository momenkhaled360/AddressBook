using AddressBook.BLL.Interfaces;
using AddressBook.BLL.Mapping;
using AddressBook.BLL.Services;
using AddressBook.DAL.Data;
using AddressBook.DAL.Repositories.Classes;
using AddressBook.DAL.Repositories.Interfaces;
using GymMangementBLL.Services.AttachmentService;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")
                ));

            services.AddAutoMapper(x =>
                x.AddProfile(new MappingProfile()));

            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IJobService, JobService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IFileService, FileService>();

            return services;
        }
    }
}