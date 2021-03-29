using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace WishMe.Service.Configs
{
  public class AuthenticationConfig
  {
    public const string _JwtIssuer = "Jwt:Issuer";
    public const string _JwtKey = "Jwt:Key";

    public static void SetupJwtBearer(JwtBearerOptions options, IConfiguration configuration)
    {
      options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = configuration[_JwtIssuer],
        ValidAudience = configuration[_JwtIssuer],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration[_JwtKey]))
      };
    }
  }
}
