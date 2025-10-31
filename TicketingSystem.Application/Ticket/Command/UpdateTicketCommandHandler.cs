using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketingSystem.Application.Common.Interfaces;
using TicketingSystem.Domain.Tickets;

namespace TicketingSystem.Application.Ticket.Command;

public class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand, bool>
{
    private readonly IAppDbContext _db;
    public UpdateTicketCommandHandler(IAppDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(UpdateTicketCommand req, CancellationToken ct)
    {
        var ticketById = await _db.Tickets.FirstOrDefaultAsync(x => x.Id == req.TicketId, ct);
        if (ticketById is null) return false;

        if (!string.IsNullOrWhiteSpace(req.Status) &&
            Enum.TryParse<TicketStatus>(req.Status, true, out var st))
            ticketById.Status = st;

        ticketById.AssignedToUserId = req.AssignedToUserId;

        await _db.SaveChangesAsync(ct);
        return true;
    }
}
