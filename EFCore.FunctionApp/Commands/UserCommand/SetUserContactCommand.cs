namespace EfCoreFunctionApp.Commands.UserCommand;

public record SetUserContactCommand(Guid Id, string FirstName, string LastName, string Email, string PhoneNumber);
