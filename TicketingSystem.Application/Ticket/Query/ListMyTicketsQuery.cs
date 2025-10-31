using MediatR;

namespace TicketingSystem.Application.Ticket.Query;

public record ListMyTicketsQuery(Guid UserId, string? Status)
    : IRequest<List<MyTicketItemDto>>;

public class MyTicketItemDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Status { get; set; } = default!;
    public string Priority { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
}
