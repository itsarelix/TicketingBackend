using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystem.Domain.Tickets;

namespace TicketingSystem.Infrastructure.Persistence.Configurations
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> t)
        {
            t.ToTable("Tickets");
            t.HasKey(x => x.Id);

            t.Property(x => x.Title).IsRequired().HasMaxLength(150);
            t.Property(x => x.Description).IsRequired().HasMaxLength(2000);
            t.Property(x => x.Status).IsRequired();
            t.Property(x => x.Priority).IsRequired();

            t.HasOne(x => x.CreatedByUser)
             .WithMany(u => u.CreatedTickets)
             .HasForeignKey(x => x.CreatedByUserId)
             .OnDelete(DeleteBehavior.Restrict);

            t.HasOne(x => x.AssignedToUser)
             .WithMany(u => u.AssignedTickets)
             .HasForeignKey(x => x.AssignedToUserId)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
