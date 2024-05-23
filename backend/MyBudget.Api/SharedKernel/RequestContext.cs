using MyBudget.SharedKernel;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace MyBudget.Api.SharedKernel;

public sealed class RequestContest(IHttpContextAccessor httpContextAccessor) : IRequestContext
{
    public Guid UserId
    {
        get
        {
            var sub = httpContextAccessor.HttpContext?.User.GetClaim(Claims.Subject);
            if (sub is null)
            {
                throw new ArgumentNullException(sub);
            }

            return Guid.Parse(sub);
        }
    }
}