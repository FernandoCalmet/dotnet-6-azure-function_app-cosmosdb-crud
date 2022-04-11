using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;
using Tasks.FunctionApp.Enums;

namespace Tasks.FunctionApp.DataTransferObjects.User;

public class UserForCreation
{
    [JsonConverter(typeof(StringEnumConverter))]
    public UserRole Role { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
