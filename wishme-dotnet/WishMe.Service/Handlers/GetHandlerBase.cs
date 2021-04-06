using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using WishMe.Service.Entities;
using WishMe.Service.Exceptions;
using WishMe.Service.Repositories;
using WishMe.Service.Requests;
using WishMe.Service.Services;

namespace WishMe.Service.Handlers
{
  public abstract class GetHandlerBase<TRequest, TEntity, TModel>: IRequestHandler<TRequest, TModel>
    where TRequest : GetRequestBase<TModel>
    where TEntity : AccessibleEntityBase
  {
    private readonly IGenericRepository fGenericRepository;
    private readonly IIdentityService fIdentityService;

    protected GetHandlerBase(
      IGenericRepository genericRepository,
      IIdentityService identityService)
    {
      fGenericRepository = genericRepository;
      fIdentityService = identityService;
    }

    public async Task<TModel> Handle(TRequest request, CancellationToken cancellationToken)
    {
      var entity = await fGenericRepository.GetAccessibleAsync<TEntity>(request.Id, cancellationToken);
      if (entity is null)
        throw new NotFoundException($"Entity of type '{typeof(TEntity)}' with ID '{request.Id}' was not found.");

      if (!await fIdentityService.CanAccessAsync(entity, cancellationToken))
        throw new ForbiddenException($"Current user does not have permissions to view resource with ID '{request.Id}'.");

      return entity.Adapt<TModel>();
    }
  }
}
