namespace Finance.Domain.Models.Configs
{
    public class JwtConfiguration
    {
        public JwtConfiguration() { }

        public JwtConfiguration(string jwtKey, string? issuer)
        {
            JwtKey = jwtKey;
            Issuer = issuer;
        }

        public string? JwtKey { get; set; }
        public string? Issuer { get; set; }
    }
}