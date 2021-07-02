using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using WishMe.Service.Models;
using WishMe.Service.Models.Items;
using WishMe.Service.Requests.Items;

namespace WishMe.Service.Controllers
{
  [Route("api/v1/items", Name = "Položky")]
  [ApiController]
  public class ItemsController: ControllerBase
  {
    private readonly IMediator fMediator;

    public ItemsController(IMediator mediator)
    {
      fMediator = mediator;
    }

    /// <summary>
    /// Vrátí seznam položek daného seznamu.
    /// </summary>
    /// <param name="wishlistId">ID seznamu přání</param>
    /// <param name="offset">počet položek, které se mají přeskočit</param>
    /// <param name="limit">počet položek, které se mají vrátit</param>
    /// <param name="cancellationToken"></param>
    /// <returns>seznam položek</returns>
    [HttpGet]
    [Authorize(Policy = AuthorizationConstants.Policies._Participant)]
    [ProducesResponseType(typeof(ListModel<ItemPreviewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetManyAsync([FromQuery] int offset, [FromQuery] int limit, [FromQuery] ObjectId wishlistId, CancellationToken cancellationToken)
    {
      return Ok(await fMediator.Send(new GetManyRequest
      {
        WishlistId = wishlistId,
        Model = new QueryModel
        {
          Offset = offset,
          Limit = limit
        }
      }, cancellationToken));
    }

    /// <summary>
    /// Vrátí detail položky.
    /// </summary>
    /// <param name="id">ID položky</param>
    /// <param name="cancellationToken"></param>
    /// <returns>detail položky</returns>
    [HttpGet("{id}")]
    [Authorize(Policy = AuthorizationConstants.Policies._Participant)]
    [ProducesResponseType(typeof(ItemDetailModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync([FromRoute] ObjectId id, CancellationToken cancellationToken)
    {
      return Ok(await fMediator.Send(new GetRequest { Id = id }, cancellationToken));
    }

    /// <summary>
    /// Upraví položku.
    /// </summary>
    /// <param name="id">ID položky</param>
    /// <param name="model">model upravené položky</param>
    /// <param name="cancellationToken"></param>
    /// <returns>prázdnou odpověď, pokud byla položky upravena</returns>
    [HttpPut("{id}")]
    [Authorize(Policy = AuthorizationConstants.Policies._Organizer)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutAsync([FromRoute] ObjectId id, [FromBody] ItemProfileModel model, CancellationToken cancellationToken)
    {
      await fMediator.Send(new PutRequest
      {
        Id = id,
        Model = model
      }, cancellationToken);

      return NoContent();
    }

    /// <summary>
    /// Zamluví položku pro přihlášeného uživatele.
    /// </summary>
    /// <param name="id">ID položky</param>
    /// <param name="model">model zamluvení položky</param>
    /// <param name="cancellationToken"></param>
    /// <returns>prázdnou odpověď, pokud byla položky zamluvena</returns>
    [HttpPut("{id}/claimed")]
    [Authorize(Policy = AuthorizationConstants.Policies._Participant)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutClaimedAsync([FromRoute] ObjectId id, [FromBody] ItemClaimedModel model, CancellationToken cancellationToken)
    {
      await fMediator.Send(new PutClaimedRequest
      {
        Id = id,
        Model = model
      }, cancellationToken);

      return NoContent();
    }

    /// <summary>
    /// Smaže položku.
    /// </summary>
    /// <param name="id">ID položky</param>
    /// <param name="cancellationToken"></param>
    /// <returns>prázdnou odpověď, pokud byla položka smazána</returns>
    [HttpDelete("{id}")]
    [Authorize(Policy = AuthorizationConstants.Policies._Organizer)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync([FromRoute] ObjectId id, CancellationToken cancellationToken)
    {
      await fMediator.Send(new DeleteRequest
      {
        Id = id
      }, cancellationToken);

      return NoContent();
    }
  }
}
