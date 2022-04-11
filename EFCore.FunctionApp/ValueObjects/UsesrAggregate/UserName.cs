namespace EfCoreFunctionApp.ValueObjects.UserAggregate;

public record UserName
{
    public string Value { get; init; }

    internal UserName(string value)
    {        
        Value = value;
    }

    public static UserName Create(string value)
    {
        Validate(value);
        return new UserName(value);
    }

    public static implicit operator string(UserName name)
    {
        return name.Value;
    }

    private static void Validate(string value)
    {
        if (value.Length > 16)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "El nombre de usuario no puede tener más de 16 caracteres.");
        }
    }
}
