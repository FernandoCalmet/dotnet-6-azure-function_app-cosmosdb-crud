using System.Collections.Generic;
using Tasks.FunctionApp.Models;

namespace Tasks.FunctionApp.DataTransferObjects.Note;

public class NoteForCreation
{
    public UserModel User { get; set; }

    public List<TaskModel> Tasks { get; set; }
}
