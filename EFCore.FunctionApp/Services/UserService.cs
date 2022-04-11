using EfCoreFunctionApp.Entities;

namespace EfCoreFunctionApp.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task HandleCommandAsync(CreateUserCommand command)
    {
        var user = new User(UserId.Create(command.Id));
        user.SetPartitionKey(command.Type);
        user.SetUserToken(UserToken.Create(command.UserToken));
        user.SetUserName(UserName.Create(command.UserName));
        user.SetUserPassword(UserPassword.Create(command.Password));
        user.SetUserContact(UserContact.Create(command.FirstName, command.LastName, command.Email, command.PhoneNumber));
        await _userRepository.AddAsync(user);
    }

    public async Task HandleCommandAsync(SetUserPartitionKeyCommand command)
    {
        var user = await _userRepository.GetAsync(UserId.Create(command.Id));
        user.SetPartitionKey(command.Type);
        await _userRepository.UpdateAsync(user);
    }

    public async Task HandleCommandAsync(SetUserTokenCommand command)
    {
        var user = await _userRepository.GetAsync(UserId.Create(command.Id));
        user.SetUserToken(UserToken.Create(command.UserToken));
        await _userRepository.UpdateAsync(user);
    }

    public async Task HandleCommandAsync(SetUserNameCommand command)
    {
        var user = await _userRepository.GetAsync(UserId.Create(command.Id));
        user.SetUserName(UserName.Create(command.UserName));
        await _userRepository.UpdateAsync(user);
    }

    public async Task HandleCommandAsync(SetUserPasswordCommand command)
    {
        var user = await _userRepository.GetAsync(UserId.Create(command.Id));
        user.SetUserPassword(UserPassword.Create(command.Password));
        await _userRepository.UpdateAsync(user);
    }

    public async Task HandleCommandAsync(SetUserContactCommand command)
    {
        var user = await _userRepository.GetAsync(UserId.Create(command.Id));
        user.SetUserContact(UserContact.Create(command.FirstName, command.LastName, command.Email, command.PhoneNumber));
        await _userRepository.UpdateAsync(user);
    }

    public async Task<User> HandleCommandAsync(GetUserIdentityGuidCommand command)
    {
        return await _userRepository.GetAsync(UserId.Create(command.Id));
    }

    public async Task<IEnumerable<User>> HandleCommandAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task HandleCommandAsync(DeleteUserCommand command)
    {
        var user = await _userRepository.GetAsync(UserId.Create(command.Id));
        await _userRepository.RemoveAsync(user);
    }
}
