using MyBudget.SharedKernel;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace MyBudget.Api.SharedKernel;

public sealed class DummyRequestContext : IRequestContext
{
    public Guid UserId { get; } = Guid.Parse("9e56ca7a-671b-48ff-a739-8f35acdf58a3");
}

public sealed class RequestContest : IRequestContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RequestContest(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId
    {
        get
        {
            var sub = _httpContextAccessor.HttpContext?.User.GetClaim(Claims.Subject);
            if (sub is null)
            {
                throw new ArgumentNullException(sub);
            }

            return Guid.Parse(sub);

        }
    }
}