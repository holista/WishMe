using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WishMe.Service.Entities;
using WishMe.Service.Exceptions;
using WishMe.Service.Repositories;
using WishMe.Service.Requests;

namespace WishMe.Service.Handlers
{
  public abstract class DeleteHandlerBase<TRequest, TEntity>: IRequestHandler<TRequest>
    where TRequest : DeleteRequestBase
    where TEntity : DbDocBase
  {
    private readonly IGenericRepository fGenericRepository;

    protected DeleteHandlerBase(IGenericRepository genericRepository)
    {
      fGenericRepository = genericRepository;
    }

    public async Task<Unit> Handle(TRequest request, CancellationToken cancellationToken)
    {
      if (!await fGenericRepository.DeleteAsync<TEntity>(request.Id, cancellationToken))
        throw new NotFoundException($"Entity of type '{typeof(TEntity)}' with ID '{request.Id}' was not found.");

      return Unit.Value;
    }
  }
}
