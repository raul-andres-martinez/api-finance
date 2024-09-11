using Finance.Domain.Factories;
using Microsoft.Extensions.Configuration;

namespace Finance.Test.Mocks.Configs
{
    public static class AppConfigMock
    {
        public static void Mock()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    { "AppConfig:EncryptionKey", "MockedEncryptionKey" },
                    { "AppConfig:JwtConfigs:JwtKey", "12345678901234561234567890123456" },
                    { "AppConfig:JwtConfigs:Issuer", "MockedIssuer" }
                })
                .Build();

            AppConfigFactory.CreateAppConfig(configuration);
        }
    }
}