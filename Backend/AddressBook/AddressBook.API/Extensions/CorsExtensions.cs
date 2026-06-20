namespace AddressBook.API.Extensions
{
    public static class CorsExtensions
    {
        public const string AngularPolicy = "AllowAngular";

        public static IServiceCollection AddCorsPolicy(
            this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(AngularPolicy,
                    policy =>
                    {
                        //policy.WithOrigins("http://localhost:4200")
                         policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });

            return services;
        }
    }
}