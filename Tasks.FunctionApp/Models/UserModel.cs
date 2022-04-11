using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using Tasks.FunctionApp.Enums;

namespace Tasks.FunctionApp.Models;

public class UserModel
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; } = Guid.NewGuid().ToString("n");
    [JsonConverter(typeof(StringEnumConverter))]
    public UserRole Role { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
