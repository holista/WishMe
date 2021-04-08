using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using WishMe.Service.Attributes;
using WishMe.Service.Configs;
using WishMe.Service.Entities;
using WishMe.Service.Extensions;
using WishMe.Service.Models.Login;

namespace WishMe.Service.Services
{
  [Lifetime(ServiceLifetime.Singleton)]
  public class IdentityService: IIdentityService
  {
    private const string _AccessCode = "accessCode";

    private static int fAccessCodeSubId = 1;

    private readonly IHttpContextAccessor fHttpContextAccessor;
    private readonly IAuthenticationConfig fAuthenticationConfig;
    private readonly IMemoryCache fMemoryCache;

    public IdentityService(
      IHttpContextAccessor httpContextAccessor,
      IAuthenticationConfig authenticationConfig,
      IMemoryCache memoryCache)
    {
      fHttpContextAccessor = httpContextAccessor;
      fAuthenticationConfig = authenticationConfig;
      fMemoryCache = memoryCache;
    }

    public bool TryGetOrganizerId(out int? organizerId)
    {
      var claims = GetClaims();
      if (!claims.IsInRole(AuthorizationConstants.Roles._Organizer))
      {
        organizerId = null;
        return false;
      }

      organizerId = int.Parse(GetSubjectId());
      return true;
    }

    public Organizer CreateOrganizer(LoginOrganizerModel model)
    {
      var provider = new RNGCryptoServiceProvider();
      var saltBytes = new byte[16];

      provider.GetBytes(saltBytes);

      var salt = Convert.ToBase64String(saltBytes);

      return new Organizer
      {
        Username = model.Username,
        PasswordHash = GeneratePasswordHash(model.Password, salt),
        SecuritySalt = salt
      };
    }

    public LoginOrganizerResponseModel? Login(string password, Organizer organizer)
    {
      var passwordHash = GeneratePasswordHash(password, organizer.SecuritySalt);
      if (passwordHash != organizer.PasswordHash)
        return null;

      var expirationUtc = DateTime.UtcNow.AddDays(1);

      var model = new LoginOrganizerResponseModel
      {
        ExpirationUtc = expirationUtc.ToUnixTimeSeconds(),
      };

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(fAuthenticationConfig.JwtKey));
      var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      var claims = new List<Claim>
      {
        new(ClaimTypes.Name,organizer.Id.ToString()),
        new(ClaimTypes.Role, AuthorizationConstants.Roles._Organizer)
      };

      var token = new JwtSecurityToken(
        claims: claims,
        expires: expirationUtc,
        signingCredentials: credentials);

      model.Token = new JwtSecurityTokenHandler().WriteToken(token);

      return model;
    }

    public LoginParticipantResponseModel Login(string accessCode)
    {
      var expirationUtc = DateTime.UtcNow.AddDays(1);

      var model = new LoginParticipantResponseModel
      {
        ExpirationUtc = expirationUtc.ToUnixTimeSeconds(),
      };

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(fAuthenticationConfig.JwtKey));
      var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

#warning TODO: tohle neni uplne optimalni, ale zatim v poho
      var nextSubId = Interlocked.Increment(ref fAccessCodeSubId);

      var claims = new List<Claim>
      {
        new(_AccessCode, accessCode),
        new(ClaimTypes.Name, nextSubId.ToString()),
        new(ClaimTypes.Role, AuthorizationConstants.Roles._Participant)
      };

      var token = new JwtSecurityToken(
        claims: claims,
        expires: expirationUtc,
        signingCredentials: credentials);

      model.Token = new JwtSecurityTokenHandler().WriteToken(token);

      return model;
    }

    public bool CanAccess(Event @event)
    {
      if (IsOrganizer())
        return true;

      var user = GetClaims();

      var accessCode = user.Claims
        .Single(claim => claim.Type == _AccessCode)
        .Value;

      return accessCode == @event.AccessCode;
    }

    private bool IsOrganizer()
    {
      var user = GetClaims();

      return user.IsInRole(AuthorizationConstants.Roles._Organizer);
    }

    private ClaimsPrincipal GetClaims()
    {
      var subjectId = GetSubjectId();

      return fMemoryCache.GetOrCreate(subjectId, FetchIdentity);
    }

    private string GetSubjectId()
    {
      if (!(fHttpContextAccessor.HttpContext is { User: { Identity: { IsAuthenticated: true } } }))
        throw new InvalidOperationException("Žádná identita.");

      return fHttpContextAccessor.HttpContext.User.Identity!.Name!;
    }

    private ClaimsPrincipal FetchIdentity(ICacheEntry entry)
    {
      var user = fHttpContextAccessor.HttpContext!.User;

      entry.SlidingExpiration = TimeSpan.FromHours(1);

      return user;
    }

    private static string GeneratePasswordHash(string password, string securitySalt)
    {
      byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
      byte[] saltBytes = Convert.FromBase64String(securitySalt);

      var algorithm = new SHA256Managed();

      var concatenated = passwordBytes
        .Concat(saltBytes)
        .ToArray();

      return Convert.ToBase64String(algorithm.ComputeHash(concatenated));
    }
  }
}