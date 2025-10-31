using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystem.Application.Common.Interfaces;

namespace TicketingSystem.Application.Ticket.Query
{
    public class ListAccessibleTicketsHandler
        : IRequestHandler<ListAccessibleTicketsQuery, List<TicketListItemDto>>
    {
        private readonly IAppDbContext _db;
        public ListAccessibleTicketsHandler(IAppDbContext db)
        {
            _db = db;
        }

        public async Task<List<TicketListItemDto>> Handle(ListAccessibleTicketsQuery req, CancellationToken ct)
        {
            var tickets = _db.Tickets.AsNoTracking();

            if (!req.IsAdmin)
                tickets = tickets.Where(t => t.CreatedByUserId == req.UserId && t.Id == req.id);
            else
                tickets = tickets.Where(t => t.AssignedToUserId == req.UserId && t.Id == req.id);

            return await tickets
                .OrderByDescending(t => t.CreatedAt)
                .Select(t => new TicketListItemDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Status = t.Status.ToString(),
                    Priority = t.Priority.ToString(),
                    CreatedAt = t.CreatedAt
                })
                .ToListAsync(ct);
        }
    }
}
