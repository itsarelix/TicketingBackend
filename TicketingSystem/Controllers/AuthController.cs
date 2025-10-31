using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TicketingSystem.Application.Auth;

namespace TicketingSystem.API.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    public AuthController(IMediator mediator) => _mediator = mediator;

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest req, CancellationToken ct)
    {
        try
        {
            var result = await _mediator.Send(new LoginCommand(req.Email, req.Password), ct);
            return Ok(result);
        }
        catch
        {
            return Unauthorized(new { message = "Invalid credentials" });
        }
    }
}
