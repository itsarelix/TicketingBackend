using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TicketingSystem.Application.Common.Helpers;
using TicketingSystem.Application.Ticket.Command;
using TicketingSystem.Application.Ticket.Query;

namespace TicketingSystem.API.Controllers;

[ApiController]
[Route("tickets")]
public class TicketsController : ControllerBase
{
    private readonly IMediator _mediator;
    public TicketsController(IMediator mediator)
    {
        _mediator = mediator;
    }


    //Create a new ticket (Employee only)
    [HttpPost]
    [Authorize(Roles = "Employee")]
    public async Task<ActionResult<CreateTicketResponse>> Create(
        [FromBody] CreateTicketRequest req,
        CancellationToken ct)
    {
        var sub = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (sub is null || !Guid.TryParse(sub, out var userId))
            return Unauthorized(new { message = "Invalid token" });

        var cmd = new CreateTicketCommand(userId, req.Title, req.Description, req.Priority);
        var result = await _mediator.Send(cmd, ct);

        return Ok(result);
    }

    //List tickets created by the current user (Employee)
    [HttpGet("my")]
    [Authorize(Roles = "Employee")]
    public async Task<ActionResult<List<MyTicketItemDto>>> My([FromQuery] string? status, CancellationToken ct)
    {
        var sub = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (sub is null || !Guid.TryParse(sub, out var userId))
            return Unauthorized(new { message = "Invalid token" });

        var list = await _mediator.Send(new ListMyTicketsQuery(userId, status), ct);
        return Ok(list);
    }

    //List all tickets (Admin only)
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<AdminTicketItemDto>>> All( CancellationToken ct)
    {
        var list = await _mediator.Send(new ListAllTicketsQuery(), ct);
        return Ok(list);
    }

    //Update ticket status and assignment(Admin only)
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTicketRequest body, CancellationToken ct)
    {
        var ok = await _mediator.Send(new UpdateTicketCommand(id, body.Status, body.AssignedToUserId), ct);
        return ok ? NoContent() : NotFound();
    }


    //Show ticket counts by status (Admin only)
    [HttpGet("stats")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Dictionary<string, int>>> Stats(CancellationToken ct)
    {
        var data = await _mediator.Send(new TicketStatsQuery(), ct);
        return Ok(data);
    }

    //Get a specific ticket’s details (allowed to creator and assigned admin)
    [HttpGet("tickets/{id}")]
    [Authorize]
    public async Task<ActionResult<List<TicketListItemDto>>> ListMineOrAssigned(Guid id, CancellationToken ct)
    {
        var (userId, isAdmin) = UserHelper.GetUserInfo(User);
        if (userId == Guid.Empty) return Unauthorized();

        var items = await _mediator.Send(new ListAccessibleTicketsQuery(id, userId, isAdmin), ct);
        return Ok(items);
    }

    //Delete a ticket (Admin only)
    [HttpDelete("tickets/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var ok = await _mediator.Send(new DeleteTicketCommand(id), ct);
        return ok ? NoContent() : NotFound();
    }
}
