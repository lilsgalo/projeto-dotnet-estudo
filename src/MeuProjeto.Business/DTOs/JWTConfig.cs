namespace MeuProjeto.Business.DTOs
{
    public class JwtConfig
    {
        public JwtConfig(string secret, string issuer, string validOn, int expireMinutes)
        {
            Secret = secret;
            Issuer = issuer;
            ValidOn = validOn;
            ExpireMinutes = expireMinutes;
        }
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string ValidOn { get; set; }
        public int ExpireMinutes { get; set; }
    }
}
