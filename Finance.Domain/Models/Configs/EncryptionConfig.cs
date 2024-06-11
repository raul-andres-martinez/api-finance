namespace Finance.Domain.Models.Configs
{
    public class EncryptionConfigs
    {
        public EncryptionConfigs(string encryptionKey)
        {
            EncryptionKey = encryptionKey;
        }

        public string EncryptionKey { get; set; }
    }

}
