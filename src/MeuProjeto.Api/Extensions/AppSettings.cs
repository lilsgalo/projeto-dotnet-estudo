namespace MeuProjeto.Api.Extensions
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int ExpireMinutes { get; set; }
        public int RefreshTokenExpireMinutes { get; set; }
        public string Issuer { get; set; }
        public string ValidOn { get; set; }
        public string UrlBase { get; set; }
        public int SSLPort { get; set; }
    }
}