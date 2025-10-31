using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketingSystem.Application.Common.Interfaces;
using TicketingSystem.Domain.Tickets;

namespace TicketingSystem.Application.Ticket.Query;

public class ListAllTicketsQueryHandler
    : IRequestHandler<ListAllTicketsQuery, List<AdminTicketItemDto>>
{
    private readonly IAppDbContext _db;
    public ListAllTicketsQueryHandler(IAppDbContext db)
    {
        _db = db;
    }

    public async Task<List<AdminTicketItemDto>> Handle(ListAllTicketsQuery req, CancellationToken ct)
    {
        var allTickets = _db.Tickets.AsNoTracking()
            .Include(t => t.CreatedByUser)
            .Include(t => t.AssignedToUser);

        return await allTickets.OrderByDescending(t => t.CreatedAt)
            .Select(t => new AdminTicketItemDto
            {
                Id = t.Id,
                Title = t.Title,
                Status = t.Status.ToString(),
                Priority = t.Priority.ToString(),
                CreatedBy = t.CreatedByUser.FullName,
                AssignedTo = t.AssignedToUser != null ? t.AssignedToUser.FullName : null,
                CreatedAt = t.CreatedAt
            })
            .ToListAsync(ct);
    }
}
