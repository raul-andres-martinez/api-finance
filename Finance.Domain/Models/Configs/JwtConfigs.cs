namespace Finance.Domain.Models.Configs
{
    public class JwtConfigs
    {
        public JwtConfigs() { }

        public JwtConfigs(string jwtKey, string? issuer)
        {
            JwtKey = jwtKey;
            Issuer = issuer;
        }

        public string? JwtKey { get; set; }
        public string? Issuer { get; set; }
    }
}