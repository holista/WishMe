using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace WishMe.Service.Configs
{
  public class AuthenticationConfig: IAuthenticationConfig
  {
    public string JwtKey { get; } = Environment.GetEnvironmentVariable(EnvVariables._JwtKey)!;

    public static void SetupJwtBearer(JwtBearerOptions options)
    {
      options.TokenValidationParameters = new TokenValidationParameters
      {
        NameClaimType = ClaimTypes.Name,
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable(EnvVariables._JwtKey)!))
      };

      if (bool.TryParse(Environment.GetEnvironmentVariable(EnvVariables._JwtDebugMode)!, out bool oauthDebugMode) && oauthDebugMode)
      {
        options.Events = new JwtBearerEvents
        {
          OnForbidden = context =>
          {
            Console.WriteLine(context.Response);
            return Task.CompletedTask;
          },
          OnAuthenticationFailed = context =>
          {
            Console.WriteLine(context.Exception);
            return Task.CompletedTask;
          },
          OnTokenValidated = context =>
          {
            Console.WriteLine(context.SecurityToken);
            return Task.CompletedTask;
          },
          OnChallenge = context =>
          {
            Console.WriteLine(context.Error);
            return Task.CompletedTask;
          },
          OnMessageReceived = context =>
          {
            Console.WriteLine(context.Token);
            return Task.CompletedTask;
          }
        };
      }
    }
  }
}
