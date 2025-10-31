namespace TicketingSystem.Application.Common.Interfaces
{
    public interface IJwtTokenService
    {
      string CreateToken(Guid userId, string fullName, string email, string role);

    }
}