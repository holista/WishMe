using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using WishMe.Service.Dtos;
using WishMe.Service.Entities;
using WishMe.Service.Models;
using WishMe.Service.Repositories;
using WishMe.Service.Requests;
using WishMe.Service.Services;
using WishMe.Service.Services.Identity;

namespace WishMe.Service.Handlers
{
  public abstract class GetManyHandlerBase<TRequest, TEntity, TModel>: IRequestHandler<TRequest, ListModel<TModel>>
    where TRequest : GetManyRequestBase<TModel>
    where TEntity : DbDocBase
  {
    protected readonly IGenericRepository fGenericRepository;
    protected readonly IIdentityService fIdentityService;

    protected GetManyHandlerBase(
      IGenericRepository genericRepository,
      IIdentityService identityService)
    {
      fGenericRepository = genericRepository;
      fIdentityService = identityService;
    }

    public async Task<ListModel<TModel>> Handle(TRequest request, CancellationToken cancellationToken)
    {
      await CheckAccessibilityAsync(request, cancellationToken);

      var filter = DoCreateFilter(request);

      filter.Offset = request.Model.Offset;
      filter.Limit = request.Model.Limit;

      var listDto = await fGenericRepository.GetManyAsync(filter, cancellationToken);

      return new ListModel<TModel>
      {
        TotalCount = listDto.TotalCount,
        Models = listDto.Entities.Adapt<List<TModel>>()
      };
    }

    protected abstract FilterDto<TEntity> DoCreateFilter(TRequest request);

    protected virtual Task CheckAccessibilityAsync(TRequest request, CancellationToken cancellationToken)
    {
      return Task.CompletedTask;
    }
  }
}
