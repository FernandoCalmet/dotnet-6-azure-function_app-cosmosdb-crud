using Newtonsoft.Json;
using System;

namespace Tasks.FunctionApp.Models;

public class TaskModel
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; } = Guid.NewGuid().ToString("n");
    public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
    public string Category { get; set; }
    public string TaskDescription { get; set; }
    public bool IsCompleted { get; set; }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
