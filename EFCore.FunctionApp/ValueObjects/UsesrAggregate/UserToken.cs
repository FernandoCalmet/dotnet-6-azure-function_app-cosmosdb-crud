namespace EfCoreFunctionApp.ValueObjects.UserAggregate;

public record UserToken
{
    public string Value { get; init; }

    internal UserToken(string value)
    {
        Value = value;
    }

    public static UserToken Create(string value)
    {
        Validate(value);
        return new UserToken(value);
    }

    private static void Validate(string value)
    {
        if (value.Length > 20)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "El Token del usuario no puede tener más de 20 caracteres.");
        }
    }
}
