namespace Tasks.FunctionApp.DataTransferObjects.Task;

public class TaskForUpdate
{
    public string Category { get; set; }
    public string TaskDescription { get; set; }
    public bool IsCompleted { get; set; }
}
