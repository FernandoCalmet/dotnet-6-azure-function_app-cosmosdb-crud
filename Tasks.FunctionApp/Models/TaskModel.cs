using Newtonsoft.Json;
using System;

namespace Tasks.FunctionApp.Models;

public class TaskModel
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; } = Guid.NewGuid().ToString("n");

    [JsonProperty(PropertyName = "createdTime")]
    public DateTime CreatedTime { get; set; } = DateTime.UtcNow;

    [JsonProperty(PropertyName = "category")]
    public string Category { get; set; }

    [JsonProperty(PropertyName = "taskDescription")]
    public string TaskDescription { get; set; }

    [JsonProperty(PropertyName = "isCompleted")]
    public bool IsCompleted { get; set; }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
