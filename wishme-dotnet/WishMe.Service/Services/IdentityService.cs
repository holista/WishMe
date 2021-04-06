using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
    private const string _Sub = "sub";

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
        new(_Sub, organizer.Id.ToString()),
        new(ClaimTypes.Name,organizer.Username),
        new(ClaimTypes.Role, AuthorizationConstants.Roles._Organizer)
      };

      var token = new JwtSecurityToken(
        claims: claims,
        expires: expirationUtc,
        signingCredentials: credentials);

      model.Token = new JwtSecurityTokenHandler().WriteToken(token);

      return model;
    }

    public LoginParticipantResponseModel Login(AccessHolder holder)
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
        new(_Sub, nextSubId.ToString()),
        new(_AccessCode, holder.Code),
        new(ClaimTypes.Role, AuthorizationConstants.Roles._Participant)
      };

      var token = new JwtSecurityToken(
        claims: claims,
        expires: expirationUtc,
        signingCredentials: credentials);

      model.Token = new JwtSecurityTokenHandler().WriteToken(token);

      return model;
    }

    public async Task<bool> IsOrganizerAsync(CancellationToken cancellationToken)
    {
      var user = await GetClaimsAsync(cancellationToken);

      return user.IsInRole(AuthorizationConstants.Roles._Organizer);
    }

    public async Task<bool> CanAccessAsync(IAccessibleEntity entity, CancellationToken cancellationToken)
    {
      if (await IsOrganizerAsync(cancellationToken))
        return true;

      var user = await GetClaimsAsync(cancellationToken);

      var accessCode = user.Claims
        .First(claim => claim.Type == _AccessCode)
        .Value;

      return accessCode == entity.AccessHolder.Code;
    }

    private async Task<ClaimsPrincipal> GetClaimsAsync(CancellationToken cancellationToken)
    {
      var subjectId = GetSubjectId();

      return await fMemoryCache.GetOrCreateAsync(subjectId, FetchIdentityAsync);
    }

    private string GetSubjectId()
    {
      if (!(fHttpContextAccessor.HttpContext is { User: { Identity: { IsAuthenticated: true } } }))
        throw new InvalidOperationException("Žádná identita.");

      return fHttpContextAccessor.HttpContext.User.Claims
        .Where(x => x.Type == _Sub)
        .Select(x => x.Value)
        .Single();
    }

    private async Task<ClaimsPrincipal> FetchIdentityAsync(ICacheEntry entry)
    {
      var user = fHttpContextAccessor.HttpContext!.User;

      entry.SlidingExpiration = TimeSpan.FromHours(1);

      return await Task.FromResult(user);
    }

    private string GeneratePasswordHash(string password, string securitySalt)
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