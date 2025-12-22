using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace FriendsService.Application.Common.Extensions {
    public static class GetRequesterIdExtension {
        public static string GetRequesterId(this ClaimsPrincipal user) {
            return user.FindFirstValue(ClaimTypes.NameIdentifier)!;
        }
    }
}
