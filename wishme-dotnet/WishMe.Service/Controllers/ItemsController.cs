using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WishMe.Service.Models.Items;
using WishMe.Service.Requests.Items;

namespace WishMe.Service.Controllers
{
  [Route("api/v1/items")]
  [ApiController]
  public class ItemsController: ControllerBase
  {
    private readonly IMediator fMediator;

    public ItemsController(IMediator mediator)
    {
      fMediator = mediator;
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
    public async Task<IActionResult> GetAsync([FromRoute] int id, CancellationToken cancellationToken)
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
    public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] ItemProfileModel model, CancellationToken cancellationToken)
    {
      await fMediator.Send(new PutRequest
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
    public async Task<IActionResult> DeleteAsync([FromRoute] int id, CancellationToken cancellationToken)
    {
      await fMediator.Send(new DeleteRequest
      {
        Id = id
      }, cancellationToken);

      return NoContent();
    }
  }
}
