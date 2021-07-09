using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Bson;
using WishMe.Service.Entities;
using WishMe.Service.Exceptions;
using WishMe.Service.Repositories;
using WishMe.Service.Requests;

namespace WishMe.Service.Handlers
{
  public abstract class PutHandlerBase<TRequest, TEntity, TModel>: IRequestHandler<TRequest>
    where TRequest : PutRequestBase<TModel>
    where TEntity : DbDocBase
  {
    protected readonly IGenericRepository fGenericRepository;

    protected PutHandlerBase(IGenericRepository genericRepository)
    {
      fGenericRepository = genericRepository;
    }

    public async Task<Unit> Handle(TRequest request, CancellationToken cancellationToken)
    {
      DoCheckModel(request.Model);

      var updated = await DoCreateUpdatedEntityAsync(request.Id, request.Model!, cancellationToken);

      if (!await fGenericRepository.UpdateAsync(request.Id, updated, cancellationToken))
        throw new NotFoundException($"Entity of type '{typeof(TEntity)}' with ID '{request.Id}' was not found.");

      return Unit.Value;
    }

    protected virtual void DoCheckModel(TModel model)
    {

    }

    protected abstract Task<TEntity> DoCreateUpdatedEntityAsync(ObjectId id, TModel model, CancellationToken cancellationToken);
  }
}
