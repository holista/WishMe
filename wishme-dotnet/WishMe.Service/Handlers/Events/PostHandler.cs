using System;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using WishMe.Service.Entities;
using WishMe.Service.Exceptions;
using WishMe.Service.Models.Events;
using WishMe.Service.Repositories;
using WishMe.Service.Requests.Events;

namespace WishMe.Service.Handlers.Events
{
  public class PostHandler: PostHandlerBase<PostRequest, Event, EventProfileModel>
  {
    public PostHandler(IGenericRepository genericRepository)
      : base(genericRepository) { }

    protected override Task DoCheckModelAsync(PostRequest request, CancellationToken cancellationToken)
    {
      if (request.Model.DateTimeUtc.Kind != DateTimeKind.Utc)
        throw new BadRequestException($"Time of the event '{request.Model.DateTimeUtc}' is not in the UTC format");

      if (request.Model.DateTimeUtc <= DateTime.UtcNow)
        throw new BadRequestException($"Time of the event '{request.Model.DateTimeUtc}' is in the past.");

      return Task.CompletedTask;
    }

    protected override async Task<int> DoFetchAccessHolderIdAsync(PostRequest request, CancellationToken cancellationToken)
    {
      var provider = new RNGCryptoServiceProvider();
      var randomBytes = new byte[16];

      provider.GetBytes(randomBytes);

      var holder = new AccessHolder { Code = Convert.ToBase64String(randomBytes) };

      return await fGenericRepository.CreateAsync(holder, cancellationToken);
    }
  }
}
