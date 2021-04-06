using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using WishMe.Service.Entities;
using WishMe.Service.Models;
using WishMe.Service.Repositories;
using WishMe.Service.Requests;

namespace WishMe.Service.Handlers
{
  public abstract class PostHandlerBase<TRequest, TEntity, TModel>: IRequestHandler<TRequest, IdModel>
    where TRequest : PostRequestBase<TModel>
    where TEntity : AccessibleEntityBase
  {
    protected readonly IGenericRepository fGenericRepository;

    protected PostHandlerBase(IGenericRepository genericRepository)
    {
      fGenericRepository = genericRepository;
    }

    public async Task<IdModel> Handle(TRequest request, CancellationToken cancellationToken)
    {
      await DoCheckModelAsync(request, cancellationToken);

      var entity = request.Model!.Adapt<TEntity>();

      entity.AccessHolderId = await DoFetchAccessHolderIdAsync(request, cancellationToken);

      DoSetAdditionalProperties(request, entity);

      var id = await fGenericRepository.CreateAsync(entity, cancellationToken);

      return new IdModel { Id = id };
    }

    protected virtual Task DoCheckModelAsync(TRequest request, CancellationToken cancellationToken)
    {
      return Task.CompletedTask;
    }

    protected virtual void DoSetAdditionalProperties(TRequest request, TEntity entity)
    {

    }

    protected abstract Task<int> DoFetchAccessHolderIdAsync(TRequest request, CancellationToken cancellationToken);
  }
}
