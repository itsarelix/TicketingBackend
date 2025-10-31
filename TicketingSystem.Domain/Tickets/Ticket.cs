using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystem.Domain.Common;
using TicketingSystem.Domain.Users;

namespace TicketingSystem.Domain.Tickets
{
    public class Ticket : BaseEntity
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public TicketStatus Status { get; set; } = TicketStatus.Open;
        public TicketPriority Priority { get; set; } = TicketPriority.Medium;
        public Guid CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; } = default!;
        public Guid? AssignedToUserId { get; set; }
        public User? AssignedToUser { get; set; }
    }
}
