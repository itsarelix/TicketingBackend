
using MediatR;
namespace TicketingSystem.Application.Auth;
public record LoginCommand(string Email, string Password) : IRequest<LoginResponse>;

