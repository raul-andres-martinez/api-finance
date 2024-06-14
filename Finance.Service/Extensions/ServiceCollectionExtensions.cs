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
            services.AddScoped<IUserRepository, UserRepository>();

            //Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
