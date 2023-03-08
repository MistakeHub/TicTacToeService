using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace TTTService.HttpApi.Helpers.JWThelper
{
    public static class JWTHelper
    {
        
        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public static ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                // ñòðîêà, ïðåäñòàâëÿþùàÿ èçäàòåëÿ
                ValidIssuer = AuthOption.ISSUER,

                // áóäåò ëè âàëèäèðîâàòüñÿ ïîòðåáèòåëü òîêåíà
                ValidateAudience = true,
                // óñòàíîâêà ïîòðåáèòåëÿ òîêåíà
                ValidAudience = AuthOption.AUDIENCE,
                // áóäåò ëè âàëèäèðîâàòüñÿ âðåìÿ ñóùåñòâîâàíèÿ
                ValidateLifetime = false,
               
           
                IssuerSigningKey = AuthOption.GetSymmetricSecurityKey(),
         
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

       
    }
}
