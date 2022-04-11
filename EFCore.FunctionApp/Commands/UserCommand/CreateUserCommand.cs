namespace EfCoreFunctionApp.Commands.UserCommand;

public record CreateUserCommand(Guid Id, string Type, string UserToken, string UserName, string Password, string FirstName, string LastName, string Email, string PhoneNumber);
