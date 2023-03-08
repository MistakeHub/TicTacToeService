using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TTTService.HttpApi.Helpers.JWThelper
{
    public class AuthOption
    {
        public const string ISSUER = "MyAuth";
        public const string AUDIENCE = "AudienceofThisToken";
        public const string SECRETKEY= "MySecretKey5473123087";
        public const int LIFETIME = 1;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SECRETKEY));
        }

    }
}
