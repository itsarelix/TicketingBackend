using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystem.Application.Common.Interfaces;

namespace TicketingSystem.Application.Ticket.Command
{
    public class DeleteTicketCommandHandler : IRequestHandler<DeleteTicketCommand, bool>
    {
        private readonly IAppDbContext _db;
        public DeleteTicketCommandHandler(IAppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Handle(DeleteTicketCommand req, CancellationToken ct)
        {
            var ticket = await _db.Tickets.FirstOrDefaultAsync(x => x.Id == req.TicketId, ct);
            if (ticket is null) return false;            

            _db.Tickets.Remove(ticket);                  
            await _db.SaveChangesAsync(ct);         

            return true;                           
        }
    }
}
