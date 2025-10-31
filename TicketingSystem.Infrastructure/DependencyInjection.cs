using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketingSystem.Application.Common.Interfaces;
using TicketingSystem.Infrastructure.Auth;
using TicketingSystem.Infrastructure.Security;


namespace TicketingSystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var cs = config.GetConnectionString("Default") ?? "Data Source=ticketing.db";
        services.AddDbContext<TicketingDbContext>(o =>
            o.UseSqlite(cs, b => b.MigrationsAssembly(typeof(TicketingDbContext).Assembly.FullName))); services.AddScoped<IAppDbContext>(sp => sp.GetRequiredService<TicketingDbContext>());
       
        services.Configure<JwtOptions>(config.GetSection("Jwt"));
        services.AddScoped<IJwtTokenService, JwtTokenService>();    
        services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();


        return services;
    }
}
