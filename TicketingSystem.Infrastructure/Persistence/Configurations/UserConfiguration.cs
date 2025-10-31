using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystem.Domain.Users;

namespace TicketingSystem.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> u)
        {
            u.ToTable("Users");
            u.HasKey(x => x.Id);

            u.Property(x => x.FullName).IsRequired().HasMaxLength(100);
            u.Property(x => x.Email).IsRequired().HasMaxLength(200);
            u.Property(x => x.Password).IsRequired();
            u.Property(x => x.Role).IsRequired();
            u.HasIndex(x => x.Email).IsUnique();
        }
    }
}
