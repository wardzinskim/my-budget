using MyBudget.Application.Services;
using MyBudget.Infrastructure.Application.Services;
using MyBudget.SharedKernel;

namespace MyBudget.Api.Tests.Mocks;

public class UserServiceMock : IUserService
{
    public UserDto? User { get; set; }

    public async Task<Result<UserDto>> FindUserAsync(string login, CancellationToken cancellationToken)
    {
        if (User is not null)
            return Result.Success(User);

        return UserService.UserLoginNotExists;
    }
}