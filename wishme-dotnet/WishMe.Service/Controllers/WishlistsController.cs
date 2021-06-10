using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using WishMe.Service.Models;
using WishMe.Service.Models.Items;
using WishMe.Service.Models.Wishlists;
using WishMe.Service.Requests.Wishlists;

namespace WishMe.Service.Controllers
{
  [Route("api/v1/wishlists", Name = "Seznamy přání")]
  [ApiController]
  public class WishlistsController: ControllerBase
  {
    private readonly IMediator fMediator;

    public WishlistsController(IMediator mediator)
    {
      fMediator = mediator;
    }

    /// <summary>
    /// Vrátí seznam seznamů přání dané události.
    /// </summary>
    /// <param name="eventId">ID události</param>
    /// <param name="offset">počet seznamů přání, které se mají přeskočit</param>
    /// <param name="limit">počet seznamů přání, které se mají vrátit</param>
    /// <param name="cancellationToken"></param>
    /// <returns>seznam seznamů přání</returns>
    [HttpGet]
    [Authorize(Policy = AuthorizationConstants.Policies._Participant)]
    [ProducesResponseType(typeof(ListModel<WishlistPreviewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetManyAsync([FromQuery] int offset, [FromQuery] int limit, [FromQuery] ObjectId eventId, CancellationToken cancellationToken)
    {
      return Ok(await fMediator.Send(new GetManyRequest
      {
        EventId = eventId,
        Model = new QueryModel
        {
          Offset = offset,
          Limit = limit
        }
      }, cancellationToken));
    }

    /// <summary>
    /// Vrátí detail seznamu přání.
    /// </summary>
    /// <param name="id">ID seznamu přání</param>
    /// <param name="cancellationToken"></param>
    /// <returns>detail seznamu přání</returns>
    [HttpGet("{id}")]
    [Authorize(Policy = AuthorizationConstants.Policies._Participant)]
    [ProducesResponseType(typeof(WishlistDetailModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync([FromRoute] ObjectId id, CancellationToken cancellationToken)
    {
      return Ok(await fMediator.Send(new GetRequest { Id = id }, cancellationToken));
    }

    /// <summary>
    /// Upraví seznam přání.
    /// </summary>
    /// <param name="id">ID seznamu přání</param>
    /// <param name="model">model upraveného seznamu přání</param>
    /// <param name="cancellationToken"></param>
    /// <returns>prázdnou odpověď, pokud byl seznam přání upraven</returns>
    [HttpPut("{id}")]
    [Authorize(Policy = AuthorizationConstants.Policies._Organizer)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutAsync([FromRoute] ObjectId id, [FromBody] WishlistProfileModel model, CancellationToken cancellationToken)
    {
      await fMediator.Send(new PutRequest
      {
        Id = id,
        Model = model
      }, cancellationToken);

      return NoContent();
    }

    /// <summary>
    /// Smaže seznam přání.
    /// </summary>
    /// <param name="id">ID seznamu přání</param>
    /// <param name="cancellationToken"></param>
    /// <returns>prázdnou odpověď, pokud byl seznam přání smazán</returns>
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

    /// <summary>
    /// Vytvoří novou položku pro daný seznam přání.
    /// </summary>
    /// <param name="id">ID seznamu přání</param>
    /// <param name="model">model nové položky</param>
    /// <param name="cancellationToken"></param>
    /// <returns>ID vytvořené položky</returns>
    [HttpPost("{id}/items")]
    [Authorize(Policy = AuthorizationConstants.Policies._Organizer)]
    [ProducesResponseType(typeof(IdModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PostWishlistAsync([FromRoute] ObjectId id, [FromBody] ItemProfileModel model, CancellationToken cancellationToken)
    {
      return Ok(await fMediator.Send(new Requests.Items.PostRequest
      {
        WishlistId = id,
        Model = model
      }, cancellationToken));
    }
  }
}
