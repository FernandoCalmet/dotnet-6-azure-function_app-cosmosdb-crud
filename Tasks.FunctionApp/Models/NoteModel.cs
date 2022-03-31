using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Tasks.FunctionApp.Models;

public class NoteModel
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; } = Guid.NewGuid().ToString("n");
    public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
    public UserModel User { get; set; }
    public List<TaskModel> Tasks { get; set; }
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
