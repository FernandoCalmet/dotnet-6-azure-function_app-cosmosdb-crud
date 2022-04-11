namespace EfCoreFunctionApp.ValueObjects.UserAggregate;

public record UserId
{
    public Guid Value { get; init; }
    internal UserId(Guid value)
    {
        Value = value;
    }

    public static UserId Create(Guid value)
    {
        Validate(value);
        return new UserId(value);
    }

    public static implicit operator Guid(UserId userId)
    {
        return userId.Value;
    }

    private static void Validate(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("El Id no puede estar vacio", nameof(value));
        }
    }
}
