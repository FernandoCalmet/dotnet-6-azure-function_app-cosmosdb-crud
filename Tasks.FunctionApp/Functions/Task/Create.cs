using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Tasks.FunctionApp.Models;
using Tasks.FunctionApp.DTO.Task;
using Tasks.FunctionApp.Exceptions;

namespace Tasks.FunctionApp.Functions.Task
{
    public class Create : Base
    {
        [FunctionName("CreateTask")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "task")] HttpRequest req,
            [CosmosDB(
                DatabaseName,
                CollectionName,
                ConnectionStringSetting = "CosmosDBConnection",
                CreateIfNotExists = true)]
                IAsyncCollector<object> tasks,
            ILogger log)
        {
            log.LogInformation("Creating a new task list item.");

            try
            {
                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<TaskForCreation>(requestBody);

                var task = new TaskModel()
                {
                    Category = data.Category,
                    TaskDescription = data.TaskDescription
                };
                await tasks.AddAsync(new
                {
                    id = task.Id,
                    created_time = task.CreatedTime,
                    category = task.Category,
                    task_description = task.TaskDescription,
                    is_completed = task.IsCompleted
                });

                log.LogInformation($"New Task created successfully with ID {task.Id}.");

                return new OkObjectResult(task);
            }
            catch (TaskException exception)
            {
                log?.LogInformation(exception.ToString());
            }

            return new BadRequestResult();
        }
    }
}
