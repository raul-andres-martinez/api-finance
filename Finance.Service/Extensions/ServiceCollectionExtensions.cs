using Finance.Domain.Interfaces.Services;
using Finance.Domain.Models.Configs;
using Finance.Persistence.Context;
using Finance.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Finance.Service.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Singleton instances
            services.AddSingletonObjects(configuration);

            //ConnString
            services.ConfigureConnectionString(configuration);

            //Jwt
            services.ConfigureJwt(configuration);

            //Cors
            services.ConfigureCors();

            //Repositories

            //Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IExpenseService, ExpenseService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }

        private static void AddSingletonObjects(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtConfiguration>(configuration.GetSection("JwtConfigs"));
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<JwtConfiguration>>().Value);

            services.Configure<EncryptionConfigs>(configuration.GetSection("EncryptionKey"));
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<EncryptionConfigs>>().Value);
        }

        private static void ConfigureConnectionString(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FinanceContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        }

        private static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtConfig = configuration.GetSection("JwtConfigs").Get<JwtConfiguration>() ?? throw new NullReferenceException();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.JwtKey ?? throw new NullReferenceException())),
                    ValidateIssuer = true,
                    ValidIssuer = jwtConfig.Issuer ?? throw new NullReferenceException(),
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
                        policy.WithOrigins("http://localhost:4200")
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials();
                    });
            });
        }
    }
}