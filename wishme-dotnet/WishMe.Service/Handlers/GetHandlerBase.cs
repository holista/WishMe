using System;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using WishMe.Service.Entities;
using WishMe.Service.Exceptions;
using WishMe.Service.Repositories;
using WishMe.Service.Requests;
using WishMe.Service.Services;
using WishMe.Service.Services.Identity;

namespace WishMe.Service.Handlers
{
  public abstract class GetHandlerBase<TRequest, TEntity, TModel>: IRequestHandler<TRequest, TModel>
    where TRequest : GetRequestBase<TModel>
    where TEntity : EntityBase
  {
    protected abstract Func<TEntity, Event> EventSelector { get; }

    protected abstract string Include { get; }

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
      var entity = await fGenericRepository.GetAsync<TEntity>(request.Id, new[] { Include }, cancellationToken);
      if (entity is null)
        throw new NotFoundException($"Entity of type '{typeof(TEntity)}' with ID '{request.Id}' was not found.");

      if (!fIdentityService.CanAccess(EventSelector(entity)))
        throw new ForbiddenException($"Current user does not have permissions to view resource with ID '{request.Id}'.");

      var model = entity.Adapt<TModel>();

      DoSetAdditionalProperties(entity, model);

      return model;
    }

    protected virtual void DoSetAdditionalProperties(TEntity entity, TModel model)
    {

    }
  }
}
