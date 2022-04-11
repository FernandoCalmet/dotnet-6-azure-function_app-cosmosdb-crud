namespace EfCoreFunctionApp.Commands.UserCommand;

public record SetUserPartitionKeyCommand(Guid Id, string Type);
