using Finance.Domain.Models.Configs;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Finance.Domain.Factories
{
    public static class AppConfigFactory
    {
        public static AppConfig CreateAppConfig(IConfiguration configuration)
        {
            var appConfigSection = configuration.GetSection("AppConfig");
            var encryptionKey = appConfigSection["EncryptionKey"];
            var jwtConfigsSection = appConfigSection.GetSection("JwtConfigs");
            var jwtKey = jwtConfigsSection["JwtKey"];
            var issuer = jwtConfigsSection["Issuer"];

            var appConfig = new AppConfig
            {
                EncryptionKey = encryptionKey!,
                JwtConfigs = new JwtConfigs
                {
                    JwtKey = jwtKey!,
                    Issuer = issuer!
                }
            };

            ValidateAppConfig(appConfig);
            return AppConfig.Initialize(appConfig);
        }

        private static void ValidateAppConfig(AppConfig appConfig)
        {
            var properties = appConfig.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                var value = property.GetValue(appConfig);

                if (value is null || (value is string str && string.IsNullOrWhiteSpace(str)))
                {
                    throw new InvalidOperationException($"Configuration property '{property.Name}' is missing or empty.");
                }
            }
        }
    }
}