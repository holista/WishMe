using System;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using WishMe.Service.Entities;
using WishMe.Service.Exceptions;
using WishMe.Service.Models.Events;
using WishMe.Service.Repositories;
using WishMe.Service.Requests.Events;
using WishMe.Service.Services;

namespace WishMe.Service.Handlers.Events
{
  public class PostHandler: PostHandlerBase<PostRequest, Event, EventProfileModel>
  {
    private readonly IIdentityService fIdentityService;

    public PostHandler(
      IGenericRepository genericRepository,
      IIdentityService identityService)
      : base(genericRepository)
    {
      fIdentityService = identityService;
    }

    protected override Task DoCheckModelAsync(PostRequest request, CancellationToken cancellationToken)
    {
      if (request.Model.DateTimeUtc.Kind != DateTimeKind.Utc)
        throw new BadRequestException($"Time of the event '{request.Model.DateTimeUtc}' is not in the UTC format");

      if (request.Model.DateTimeUtc <= DateTime.UtcNow)
        throw new BadRequestException($"Time of the event '{request.Model.DateTimeUtc}' is in the past.");

      return Task.CompletedTask;
    }

    protected override Task DoSetAdditionalPropertiesAsync(PostRequest request, Event entity, CancellationToken cancellationToken)
    {
      if (!fIdentityService.TryGetOrganizerId(out int? organizerId))
        throw new InvalidOperationException();

      entity.OrganizerId = organizerId!.Value;

      var provider = new RNGCryptoServiceProvider();
      var randomBytes = new byte[12];

      provider.GetBytes(randomBytes);

      entity.AccessCode = Convert.ToBase64String(randomBytes);

      return Task.CompletedTask;
    }
  }
}
