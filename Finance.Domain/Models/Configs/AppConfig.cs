namespace Finance.Domain.Models.Configs
{
    public class AppConfig
    {
        public required string EncryptionKey { get; init; }
        public required JwtConfigs JwtConfigs { get; init; }

        private static AppConfig? _instance;
        private static readonly object _lock = new();

        internal AppConfig() { }

        public static AppConfig Initialize(AppConfig config)
        {
            lock (_lock)
            {
                if (_instance is null)
                {
                    _instance = config;
                }
            }

            return _instance;
        }

        public static AppConfig Instance
        {
            get
            {
                if (_instance is null)
                {
                    throw new InvalidOperationException("AppConfig has not been initialized.");
                }

                return _instance;
            }
        }

    }

    public class JwtConfigs
    {
        public required string JwtKey { get; init; }
        public required string Issuer { get; init; }
    }
}