using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using WishMe.Service.Entities;
using WishMe.Service.Exceptions;
using WishMe.Service.Repositories;
using WishMe.Service.Requests;
using WishMe.Service.Services.Identity;

namespace WishMe.Service.Handlers
{
  public abstract class GetHandlerBase<TRequest, TEntity, TModel>: IRequestHandler<TRequest, TModel>
    where TRequest : GetRequestBase<TModel>
    where TEntity : DbDocBase
  {
    protected readonly IGenericRepository fGenericRepository;
    protected readonly IIdentityService fIdentityService;

    protected GetHandlerBase(
      IGenericRepository genericRepository,
      IIdentityService identityService)
    {
      fGenericRepository = genericRepository;
      fIdentityService = identityService;
    }

    public async Task<TModel> Handle(TRequest request, CancellationToken cancellationToken)
    {
      var entity = await fGenericRepository.GetAsync<TEntity>(request.Id, cancellationToken);
      if (entity is null)
        throw new NotFoundException($"Entity of type '{typeof(TEntity)}' with ID '{request.Id}' was not found.");

      var @event = await DoGetEventAsync(entity, cancellationToken);
      if (@event is null)
        throw new NotFoundException($"Event was not found for entity of type '{typeof(TEntity)}' with ID '{request.Id}'.");

      if (!fIdentityService.CanAccess(@event))
        throw new ForbiddenException($"Current user does not have permissions to view resource with ID '{request.Id}'.");

      var model = entity.Adapt<TModel>();

      DoSetAdditionalProperties(entity, model);

      return model;
    }

    protected virtual void DoSetAdditionalProperties(TEntity entity, TModel model)
    {

    }

    protected abstract Task<Event?> DoGetEventAsync(TEntity entity, CancellationToken cancellationToken);
  }
}
