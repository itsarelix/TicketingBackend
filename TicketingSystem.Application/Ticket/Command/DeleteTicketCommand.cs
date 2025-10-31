using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSystem.Application.Ticket.Command
{
    public record DeleteTicketCommand(Guid TicketId) : IRequest<bool>;

}
