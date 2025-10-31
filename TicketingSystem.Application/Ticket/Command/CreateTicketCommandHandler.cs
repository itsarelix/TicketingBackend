using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystem.Application.Common.Interfaces;
using TicketingSystem.Domain.Tickets;

namespace TicketingSystem.Application.Ticket.Command
{
    public class CreateTicketCommandHandler
       : IRequestHandler<CreateTicketCommand, CreateTicketResponse>
    {
        private readonly IAppDbContext _db;

        public CreateTicketCommandHandler(IAppDbContext db)
        {
            _db = db;
        }
        public async Task<CreateTicketResponse> Handle(CreateTicketCommand req, CancellationToken ct)
        {
            var user = await _db.Users.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == req.UserId, ct)
                ?? throw new InvalidOperationException("User not found");

            var priority = Enum.TryParse<TicketPriority>(req.Priority, true, out var pr)
                ? pr : TicketPriority.Medium;

            var ticket = new Domain.Tickets.Ticket
            {
                Title = req.Title.Trim(),
                Description = req.Description.Trim(),
                Priority = priority,
                Status = TicketStatus.Open,
                CreatedByUserId = req.UserId
            };

            _db.Tickets.Add(ticket);
            await _db.SaveChangesAsync(ct);

            return new CreateTicketResponse
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Status = ticket.Status.ToString(),
                Priority = ticket.Priority.ToString(),
                CreatedBy = user.FullName,
                CreatedAt = ticket.CreatedAt
            };
        }
    }
}
