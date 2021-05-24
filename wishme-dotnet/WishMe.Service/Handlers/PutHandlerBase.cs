using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WishMe.Service.Entities;
using WishMe.Service.Exceptions;
using WishMe.Service.Repositories;
using WishMe.Service.Requests;

namespace WishMe.Service.Handlers
{
  public abstract class PutHandlerBase<TRequest, TEntity, TModel>: IRequestHandler<TRequest>
    where TRequest : PutRequestBase<TModel>
    where TEntity : EntityBase
  {
    private readonly IGenericRepository fGenericRepository;

    protected PutHandlerBase(IGenericRepository genericRepository)
    {
      fGenericRepository = genericRepository;
    }

    public async Task<Unit> Handle(TRequest request, CancellationToken cancellationToken)
    {
      DoCheckModel(request.Model);

      if (!await fGenericRepository.UpdateAsync<TEntity>(request.Id, request.Model!, cancellationToken))
        throw new NotFoundException($"Entity of type '{typeof(TEntity)}' with ID '{request.Id}' was not found.");

      return Unit.Value;
    }

    protected abstract void DoCheckModel(TModel model);
  }
}
