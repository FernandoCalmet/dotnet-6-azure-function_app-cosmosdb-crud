using EfCoreFunctionApp.Entities;

namespace EfCoreFunctionApp.Contracts;

public interface IUserService
{
    Task HandleCommandAsync(CreateUserCommand command);
    Task HandleCommandAsync(SetUserPartitionKeyCommand command);
    Task HandleCommandAsync(SetUserTokenCommand command);
    Task HandleCommandAsync(SetUserNameCommand command);
    Task HandleCommandAsync(SetUserPasswordCommand command);
    Task HandleCommandAsync(SetUserContactCommand command);
    Task<User> HandleCommandAsync(GetUserIdentityGuidCommand command);
    Task<IEnumerable<User>> HandleCommandAsync();
    Task HandleCommandAsync(DeleteUserCommand command);
}
