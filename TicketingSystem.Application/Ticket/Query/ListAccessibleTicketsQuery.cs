using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSystem.Application.Ticket.Query
{
    public record ListAccessibleTicketsQuery(Guid id, Guid UserId, bool IsAdmin)
     : IRequest<List<TicketListItemDto>>;

    public class TicketListItemDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string Status { get; set; } = default!;
        public string Priority { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
    }
}
