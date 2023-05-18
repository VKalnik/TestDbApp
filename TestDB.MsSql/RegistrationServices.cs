using Microsoft.Extensions.DependencyInjection;
using TestDb.Interfaces;
using TestDB.MsSql.Services;

namespace TestDB.MsSql
{
    public static class RegistrationServices
    {
        public static IServiceCollection AddDbServices(this IServiceCollection services)
        {
            services.AddScoped<IDbService, DbService>();
            return services;
        }
    }
}
