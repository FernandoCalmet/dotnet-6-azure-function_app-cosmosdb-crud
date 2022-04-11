namespace EfCoreFunctionApp.ValueObjects.UserAggregate;

public record UserPassword
{
    public string Value { get; init; }

    internal UserPassword(string value)
    {
        Value = value;
    }

    public static UserPassword Create(string value)
    {
        Validate(value);
        return new UserPassword(value);
    }

    public static implicit operator string(UserPassword password)
    {
        return password.Value;
    }

    private static void Validate(string value)
    {
        if (value.Length > 20)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "La contraseña de usuario no puede tener más de 20 caracteres.");
        }
    }
}
