using Finance.Domain.Interfaces.Repositories;
using Finance.Domain.Interfaces.Services;
using Finance.Persistence.Repositories;
using Finance.Service.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Finance.Service.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Repositories
            services.AddSingleton<IUserRepository, UserRepository>();

            //Services
            services.AddSingleton<IUserService, UserService>();

            return services;
        }
    }
}
