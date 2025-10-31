using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystem.Application.Common.Interfaces;
using TicketingSystem.Domain.Tickets;
using TicketingSystem.Domain.Users;

namespace TicketingSystem.Infrastructure
{
    public class TicketingDbContext : DbContext , IAppDbContext
    {

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TicketingDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public TicketingDbContext(DbContextOptions<TicketingDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }  
        public DbSet<Ticket> Tickets { get; set; }
    }

}
