using System;
using System.Text.Json.Serialization;

namespace MeuProjeto.Business.DTOs
{
    public class AuthenticateResponse
    {
        public string AccessToken { get; set; }

        public DateTime ExpiresIn { get; set; }

        [JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }

        public AuthenticateResponse(string jwtToken, DateTime expiresIn, string refreshToken)
        {
            AccessToken = jwtToken;
            ExpiresIn = expiresIn;
            RefreshToken = refreshToken;
        }
    }
}