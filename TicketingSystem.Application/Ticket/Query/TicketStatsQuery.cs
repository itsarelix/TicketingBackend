using MediatR;

namespace TicketingSystem.Application.Ticket.Query;

public record TicketStatsQuery() : IRequest<Dictionary<string, int>>;
