namespace EfCoreFunctionApp.Entities;

public class User
{
    public Guid Id { get; init; }
    public string PartitionKey { get; private set; }
    public UserToken UserToken { get; private set; }
    public UserName UserName { get; private set; }
    public UserPassword UserPassword { get; private set; }
    public UserContact UserContact { get; private set; }

    public User() { }
    public User(UserId id) { Id = id; }

    public void SetUserToken(UserToken userToken)
    {
        UserToken = userToken;
    }

    public void SetPartitionKey(string partitionKey)
    {
        PartitionKey = partitionKey;
    }

    public void SetUserName(UserName userName)
    {
        UserName = userName;
    }

    public void SetUserPassword(UserPassword userPassword)
    {
        UserPassword = userPassword;
    }

    public void SetUserContact(UserContact userContact)
    {
        UserContact = userContact;
    }
}
