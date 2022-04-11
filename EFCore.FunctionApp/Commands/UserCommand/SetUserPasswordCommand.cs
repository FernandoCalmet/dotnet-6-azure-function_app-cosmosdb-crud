namespace EfCoreFunctionApp.Commands.UserCommand;

public record SetUserPasswordCommand(Guid Id, string Password);
