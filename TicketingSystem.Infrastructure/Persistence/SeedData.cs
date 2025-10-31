using Microsoft.EntityFrameworkCore;
using TicketingSystem.Domain.Users;

namespace TicketingSystem.Infrastructure.Persistence;

public static class SeedData
{
    public static async Task InitializeAsync(TicketingDbContext db, CancellationToken ct = default)
    {
        await db.Database.MigrateAsync(ct);

        var data = db.Users.Select(u => u.Email).ToList() ?? null;


        if (!await db.Users.AnyAsync(ct))
        {
            string Hash(string p) => BCrypt.Net.BCrypt.HashPassword(p);

            var admin = new User
            {
                FullName = "Admin",
                Email = "admin@gmail.com",
                Password = Hash("Admin@123"),
                Role = UserRole.Admin
            };

            var employee = new User
            {
                FullName = "Employee",
                Email = "employee@gmail.com",
                Password = Hash("Employee@123"),
                Role = UserRole.Employee
            };

            await db.Users.AddRangeAsync(admin, employee);
            await db.SaveChangesAsync(ct);
        }
    }
}
