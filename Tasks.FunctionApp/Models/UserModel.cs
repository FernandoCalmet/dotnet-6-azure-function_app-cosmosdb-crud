using Newtonsoft.Json;
using System;

namespace Tasks.FunctionApp.Models;

public class UserModel
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; } = Guid.NewGuid().ToString("n");
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
