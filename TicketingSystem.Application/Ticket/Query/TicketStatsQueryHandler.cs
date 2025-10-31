using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketingSystem.Application.Common.Interfaces;

namespace TicketingSystem.Application.Ticket.Query;

public class TicketStatsQueryHandler : IRequestHandler<TicketStatsQuery, Dictionary<string, int>>
{
    private readonly IAppDbContext _db;
    public TicketStatsQueryHandler(IAppDbContext db)
    {
        _db = db;
    }

    public async Task<Dictionary<string, int>> Handle(TicketStatsQuery req, CancellationToken ct)
    {
        var countList = await _db.Tickets.AsNoTracking()
        .GroupBy(t => t.Status)
        .Select(g => new { Status = g.Key, Count = g.Count() })
        .ToListAsync(ct);

        var result = countList.ToDictionary(x => x.Status.ToString(), x => x.Count);
        return result;
    }
}
