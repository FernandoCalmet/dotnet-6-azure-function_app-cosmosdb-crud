using Newtonsoft.Json;

namespace Tasks.FunctionApp.DTO;

public class TaskForCreation
{
    [JsonProperty(PropertyName = "taskDescription")]
    public string TaskDescription { get; set; }

    [JsonProperty(PropertyName = "category")]
    public string Category { get; set; }
}
