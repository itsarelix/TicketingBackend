using MediatR;

namespace TicketingSystem.Application.Ticket.Query;

public record ListAllTicketsQuery()
    : IRequest<List<AdminTicketItemDto>>;

public class AdminTicketItemDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Status { get; set; } = default!;
    public string Priority { get; set; } = default!;
    public string CreatedBy { get; set; } = default!;
    public string? AssignedTo { get; set; }
    public DateTime CreatedAt { get; set; }
}
