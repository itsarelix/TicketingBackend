using MediatR;

namespace TicketingSystem.Application.Ticket.Command;

public class UpdateTicketRequest
{
    public string? Status { get; set; }
    public Guid? AssignedToUserId { get; set; }
}

public record UpdateTicketCommand(Guid TicketId, string? Status, Guid? AssignedToUserId)
    : IRequest<bool>;
