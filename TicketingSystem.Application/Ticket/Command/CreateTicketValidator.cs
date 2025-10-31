using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSystem.Application.Ticket.Command
{
    public class CreateTicketValidator : AbstractValidator<CreateTicketRequest>
    {
        public CreateTicketValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(2000);
            RuleFor(x => x.Priority).Must(p => new[] { "Low", "Medium", "High" }
                .Contains(p, StringComparer.OrdinalIgnoreCase))
                .WithMessage("Priority must be Low, Medium, or High");
        }
    }
}
