using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketingSystem.Application.Common.Interfaces;

namespace TicketingSystem.Application.Auth;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IAppDbContext _db;
    private readonly IPasswordHasher _hasher;
    private readonly IJwtTokenService _jwt;

    public LoginCommandHandler(IAppDbContext db, IPasswordHasher hasher, IJwtTokenService jwt)
    {
        _db = db; 
        _hasher = hasher; 
        _jwt = jwt;
    }

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken ct)
    {
        var user = await _db.Users.AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == request.Email, ct);

        if (user is null || !_hasher.Verify(user.Password, request.Password))
            throw new UnauthorizedAccessException("Invalid credentials");

        var token = _jwt.CreateToken(user.Id, user.FullName, user.Email, user.Role.ToString());

        return new LoginResponse
        {
            Token = token,
            FullName = user.FullName,
            Email = user.Email,
            Role = user.Role.ToString()
        };
    }
}
