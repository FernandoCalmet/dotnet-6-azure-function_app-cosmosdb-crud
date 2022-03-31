using System.Collections.Generic;
using Tasks.FunctionApp.Models;

namespace Tasks.FunctionApp.DataTransferObjects.Note;

public class NoteForUpdate
{
    public List<TaskModel> Tasks { get; set; }
}
