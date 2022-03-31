using Newtonsoft.Json;

namespace Tasks.FunctionApp.DTO;

public class TaskForUpdate
{
    [JsonProperty(PropertyName = "category")]
    public string Category { get; set; }

    [JsonProperty(PropertyName = "taskDescription")]
    public string TaskDescription { get; set; }

    [JsonProperty(PropertyName = "isCompleted")]
    public bool IsCompleted { get; set; }
}
