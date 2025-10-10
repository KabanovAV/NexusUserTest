using System.Security.Claims;

namespace NexusUserTest.Shared.Services
{
    public class UserSessionService
    {
        public int UserId { get; private set; }
        public string? UserName { get; private set; }

        public void SetUser(ClaimsPrincipal user)
        {
            if (user.Identity is null || !user.Identity.IsAuthenticated)
                return;

            UserId = int.Parse(user.FindFirst("UserId")!.Value);
            UserName = user.Identity.Name;
        }
    }
}
