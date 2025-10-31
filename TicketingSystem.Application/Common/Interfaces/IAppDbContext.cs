using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystem.Domain.Tickets;
using TicketingSystem.Domain.Users;

namespace TicketingSystem.Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<User> Users { get; }
        DbSet<TicketingSystem.Domain.Tickets.Ticket> Tickets { get; }
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
