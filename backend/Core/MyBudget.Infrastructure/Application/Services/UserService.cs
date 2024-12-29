using Grpc.Core;
using MyBudget.Application.Services;
using MyBudget.Identity.Contract;
using UserDto = MyBudget.Application.Services.UserDto;

namespace MyBudget.Infrastructure.Application.Services;

public class UserService(Users.UsersClient usersClient) : IUserService
{
    public static Error UserLoginNotExists { get; } =
        new BadRequestError(nameof(UserLoginNotExists), "User login not exists");

    public async Task<Result<UserDto>> FindUserAsync(string login, CancellationToken cancellationToken)
    {
        try
        {
            var userResult = await usersClient.FindUserAsync(new FindUserRequest() {Email = login},
                cancellationToken: cancellationToken);

            return new UserDto(Guid.Parse(userResult.Id), userResult.Email);
        }
        catch (RpcException e)
        {
            if (e.Status.StatusCode == StatusCode.NotFound)
                return UserLoginNotExists;
            return new Error(nameof(FindUserAsync), e.Message);
        }
    }
}