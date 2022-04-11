namespace EfCoreFunctionApp.ValueObjects.UserAggregate;

public record UserContact
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }

    internal UserContact(string firstName, string lastName, string email, string phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public static UserContact Create(string firstName, string lastName, string email, string phoneNumber)
    {
        ValidateFirstName(firstName);
        ValidateLastName(lastName);
        ValidateEmail(email);
        ValidatePhoneNumber(phoneNumber);
        return new UserContact(firstName, lastName, email, phoneNumber);
    }

    private static void ValidateFirstName(string value)
    {
        if (value.Length > 30)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "El nombre no debe tener más de 30 caracteres.");
        }
    }

    private static void ValidateLastName(string value)
    {
        if (value.Length > 30)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "El apellido no debe tener más de 30 caracteres.");
        }
    }

    private static void ValidateEmail(string value)
    {
        if (value.Length > 50)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "El correo electrónico no debe tener más de 50 caracteres.");
        }
    }

    private static void ValidatePhoneNumber(string value)
    {
        if (value.Length > 15)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "El número de teléfono no debe tener más de 15 caracteres..");
        }
    }
}
