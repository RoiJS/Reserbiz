using System;

namespace ReserbizAPP.LIB.Dtos
{
    public class AuthenticationTokenInfoDto
    {
        public string AccessToken { get; set; }
        public DateTime? ExpiresIn { get; set; }
        public string RefreshToken { get; set; }
        // public AccountForDetailDto CurrentUser { get; set; }
    }
}