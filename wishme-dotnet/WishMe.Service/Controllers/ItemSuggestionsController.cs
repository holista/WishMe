using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WishMe.Service.Models.ItemSuggestions.Heureka;
using WishMe.Service.Requests.ItemSuggestions.Heureka;

namespace WishMe.Service.Controllers
{
  [ApiController]
  [Route("api/v1/items/suggestions", Name = "Doporučování položek")]
  public class ItemSuggestionsController: ControllerBase
  {
    private readonly IMediator fMediator;

    public ItemSuggestionsController(IMediator mediator)
    {
      fMediator = mediator;
    }

    /// <summary>
    /// Vrátí detailní informace o položce z URL odkazu na Heurece.
    /// </summary>
    /// <param name="url">URL odkaz na položku</param>
    /// <param name="cancellationToken"></param>
    /// <returns>detail položky z Heureky</returns>
    [HttpGet("heureka/detail")]
    [Authorize(Policy = AuthorizationConstants.Policies._Organizer)]
    [ProducesResponseType(typeof(DetailModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync([FromQuery] string url, CancellationToken cancellationToken)
    {
      return Ok(await fMediator.Send(new GetDetailRequest { Url = url }, cancellationToken));
    }
  }
}
