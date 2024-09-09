using Finance.Domain.Factories;
using Finance.Domain.Interfaces.Services;
using Finance.Domain.Models.Configs;
using Finance.Persistence.Context;
using Finance.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Finance.Service.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Singleton config instance
            services.AddAppConfig(configuration);

            //ConnString
            services.ConfigureConnectionString(configuration);

            //Jwt
            services.ConfigureJwt();

            //Cors
            services.ConfigureCors();

            //Repositories

            //Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IExpenseService, ExpenseService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }

        private static void AddAppConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var appConfig = AppConfigFactory.CreateAppConfig(configuration);
            services.AddSingleton(appConfig);
        }

        private static void ConfigureConnectionString(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FinanceContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        }

        private static void ConfigureJwt(this IServiceCollection services)
        {
            var appConfig = services.BuildServiceProvider().GetRequiredService<AppConfig>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appConfig.JwtConfigs.JwtKey)),
                    ValidateIssuer = true,
                    ValidIssuer = appConfig.JwtConfigs.Issuer,
                    ValidateAudience = false
                };
            });

            services.AddAuthorization();
        }

        private static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200") // TODO - Only for local debbug, change later
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials();
                    });
            });
        }
    }
}