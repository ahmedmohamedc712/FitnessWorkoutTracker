using Application.Abstraction;
using System.Security.Claims;

namespace PublicApi.Services
{
    public class CurrentUserAccessor(IHttpContextAccessor httpContextAccessor) : ICurrentUserAccessor
    {
        public Guid GetId()
        {
            return Guid.Parse(httpContextAccessor.HttpContext!.User
                .FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }
    }
}
