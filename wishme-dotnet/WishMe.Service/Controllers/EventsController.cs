using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WishMe.Service.Models;
using WishMe.Service.Models.Events;
using WishMe.Service.Models.Wishlists;
using WishMe.Service.Requests.Events;

namespace WishMe.Service.Controllers
{
  [ApiController]
  [Route("api/v1/events", Name = "Události")]
  public class EventsController: ControllerBase
  {
    private readonly IMediator fMediator;

    public EventsController(IMediator mediator)
    {
      fMediator = mediator;
    }

    /// <summary>
    /// Vytvoří novou událost.
    /// </summary>
    /// <param name="model">model nové události</param>
    /// <param name="cancellationToken"></param>
    /// <returns>ID vytvořené události</returns>
    [HttpPost]
    [Authorize(Policy = AuthorizationConstants.Policies._Organizer)]
    [ProducesResponseType(typeof(IdModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> PostAsync([FromBody] EventProfileModel model, CancellationToken cancellationToken)
    {
      return Ok(await fMediator.Send(new PostRequest { Model = model }, cancellationToken));
    }

    /// <summary>
    /// Vrátí detail události.
    /// </summary>
    /// <param name="id">ID události</param>
    /// <param name="cancellationToken"></param>
    /// <returns>detail události</returns>
    [HttpGet("{id}")]
    [Authorize(Policy = AuthorizationConstants.Policies._Participant)]
    [ProducesResponseType(typeof(EventDetailModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync([FromRoute] int id, CancellationToken cancellationToken)
    {
      return Ok(await fMediator.Send(new GetRequest { Id = id }, cancellationToken));
    }

    /// <summary>
    /// Upraví událost.
    /// </summary>
    /// <param name="id">ID události</param>
    /// <param name="model">model upravené události</param>
    /// <param name="cancellationToken"></param>
    /// <returns>prázdnou odpověď, pokud byla událost upravena</returns>
    [HttpPut("{id}")]
    [Authorize(Policy = AuthorizationConstants.Policies._Organizer)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] EventProfileModel model, CancellationToken cancellationToken)
    {
      await fMediator.Send(new PutRequest
      {
        Id = id,
        Model = model
      }, cancellationToken);

      return NoContent();
    }

    /// <summary>
    /// Smaže událost.
    /// </summary>
    /// <param name="id">ID události</param>
    /// <param name="cancellationToken"></param>
    /// <returns>prázdnou odpověď, pokud byla událost smazána</returns>
    [HttpDelete("{id}")]
    [Authorize(Policy = AuthorizationConstants.Policies._Organizer)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id, CancellationToken cancellationToken)
    {
      await fMediator.Send(new DeleteRequest
      {
        Id = id
      }, cancellationToken);

      return NoContent();
    }

    /// <summary>
    /// Vytvoří nový seznam přání pro danou událost.
    /// </summary>
    /// <param name="id">ID události</param>
    /// <param name="model">model nového seznamu přání</param>
    /// <param name="cancellationToken"></param>
    /// <returns>ID vytvořeného seznamu přání</returns>
    [HttpPost("{id}/wishlists")]
    [Authorize(Policy = AuthorizationConstants.Policies._Organizer)]
    [ProducesResponseType(typeof(IdModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PostWishlistAsync([FromRoute] int id, [FromBody] WishlistProfileModel model, CancellationToken cancellationToken)
    {
      return Ok(await fMediator.Send(new Requests.Wishlists.PostRequest
      {
        EventId = id,
        Model = model
      }, cancellationToken));
    }
  }
}
