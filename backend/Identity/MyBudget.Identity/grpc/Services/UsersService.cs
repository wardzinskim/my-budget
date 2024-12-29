using Grpc.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MyBudget.Identity.grpc.Services;

public class UsersService(ILogger<UsersService> logger, UserManager<IdentityUser> userManager) : Users.UsersBase
{
    private readonly ILogger<UsersService> _logger = logger;


    public override async Task<UserDto> FindUser(FindUserRequest request, ServerCallContext context)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(x =>
            x.NormalizedEmail == request.Email.ToUpperInvariant());

        if (user is not null)
        {
            return new UserDto {Email = user.Email, Id = user.Id};
        }

        throw new RpcException(new Status(StatusCode.NotFound, $"User with email {request.Email} not found"));
    }
}