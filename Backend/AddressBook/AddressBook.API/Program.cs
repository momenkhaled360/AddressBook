using AddressBook.API.Extensions;
using AddressBook.API.Middlewares;

namespace AddressBook.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddApplicationServices(builder.Configuration);
            builder.Services.AddIdentityAndJwt(builder.Configuration);
            builder.Services.AddCorsPolicy();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddOpenApi();

            var app = builder.Build();

            await app.SeedDataAsync();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseCors(CorsExtensions.AngularPolicy);

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}