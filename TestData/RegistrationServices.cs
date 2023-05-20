using Microsoft.Extensions.DependencyInjection;
using TestDb.Interfaces;

namespace TestData
{
    /// <summary>
    ///Класс расширения для регистрации сервисов библиотеки TestData.
    /// </summary>
    public static class RegistrationServices
    {
        public static IServiceCollection AddTestDataServices(this IServiceCollection services)
        {
            services.AddScoped<ITestData, TestData>();
            return services;
        }
    }
}
