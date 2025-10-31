using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketingSystem.Application.Common.Interfaces;
using TicketingSystem.Domain.Tickets;

namespace TicketingSystem.Application.Ticket.Query;

public class ListMyTicketsQueryHandler
    : IRequestHandler<ListMyTicketsQuery, List<MyTicketItemDto>>
{
    private readonly IAppDbContext _db;
    public ListMyTicketsQueryHandler(IAppDbContext db)
    {
        _db = db;
    }

    public async Task<List<MyTicketItemDto>> Handle(ListMyTicketsQuery req, CancellationToken ct)
    {
        var tickets = _db.Tickets.AsNoTracking()
            .Where(t => t.CreatedByUserId == req.UserId);

        if (!string.IsNullOrWhiteSpace(req.Status) &&
            Enum.TryParse<TicketStatus>(req.Status, true, out var st))
            tickets = tickets.Where(t => t.Status == st);

        return await tickets
            .OrderByDescending(t => t.CreatedAt)
            .Select(t => new MyTicketItemDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status.ToString(),
                Priority = t.Priority.ToString(),
                CreatedAt = t.CreatedAt
            })
            .ToListAsync(ct);
    }
}
