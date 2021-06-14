using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WishMe.Service.Models.Login;
using WishMe.Service.Requests.Login;

namespace WishMe.Service.Controllers
{
  [ApiController]
  [Route("api/v1/users", Name = "Uživatelé")]
  public class UsersController: ControllerBase
  {
    private readonly IMediator fMediator;

    public UsersController(IMediator mediator)
    {
      fMediator = mediator;
    }

    /// <summary>
    /// Registruje nového organizátora.
    /// </summary>
    /// <param name="model">Jméno a heslo organizátora.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>ID a přístupový token</returns>
    [HttpPost("register/organizer")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(LoginOrganizerResponseModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostRegisterOrganizerAsync([FromBody] LoginOrganizerModel model, CancellationToken cancellationToken)
    {
      return Ok(await fMediator.Send(new RegisterOrganizerRequest { Model = model }, cancellationToken));
    }

    /// <summary>
    /// Přihlásí existujícího organizátora.
    /// </summary>
    /// <param name="model">Jméno a heslo organizátora.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>ID a přístupový token</returns>
    [HttpPost("login/organizer")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(LoginOrganizerResponseModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> PostLoginOrganizerAsync([FromBody] LoginOrganizerModel model, CancellationToken cancellationToken)
    {
      return Ok(await fMediator.Send(new LoginOrganizerRequest { Model = model }, cancellationToken));
    }

    /// <summary>
    /// Přihlásí účastníka akce.
    /// </summary>
    /// <param name="model">Přístupový kód akce</param>
    /// <param name="cancellationToken"></param>
    /// <returns>ID akce a přístupový token</returns>
    [HttpPost("login/participant")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(LoginParticipantResponseModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PostLoginParticipantAsync([FromBody] LoginParticipantModel model, CancellationToken cancellationToken)
    {
      return Ok(await fMediator.Send(new LoginParticipantRequest { Model = model }, cancellationToken));
    }
  }
}
