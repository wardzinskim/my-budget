namespace MyBudget.Application.Services;

public interface IUserService
{
    Task<Result<UserDto>> FindUserAsync(string login, CancellationToken cancellationToken);
}

public record UserDto(Guid Id, string Login);