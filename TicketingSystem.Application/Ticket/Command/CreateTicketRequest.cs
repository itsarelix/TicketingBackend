using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSystem.Application.Ticket.Command
{
    public class CreateTicketRequest
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Priority { get; set; } = "Medium";
    }
}
