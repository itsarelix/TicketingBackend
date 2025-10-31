using MediatR;

namespace TicketingSystem.Application.Ticket.Command;

public record CreateTicketCommand(
    Guid UserId,
    string Title,
    string Description,
    string Priority
) : IRequest<CreateTicketResponse>;

