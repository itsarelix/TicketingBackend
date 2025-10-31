using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSystem.Application.Common.Helpers
{
    public static class UserHelper
    {
        public static (Guid userId, bool isAdmin) GetUserInfo(ClaimsPrincipal user)
        {
            var sub = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(sub, out var uid))
                return (Guid.Empty, false);

            var role = user.FindFirst(ClaimTypes.Role)?.Value ?? "Employee";
            var isAdmin = string.Equals(role, "Admin", StringComparison.OrdinalIgnoreCase);

            return (uid, isAdmin);
        }
    }
}
