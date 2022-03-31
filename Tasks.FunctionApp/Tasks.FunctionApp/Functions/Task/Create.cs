using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Tasks.FunctionApp.Models;
using Tasks.FunctionApp.DTO;

namespace Tasks.FunctionApp.Functions.Task
{
    public class Create : Base
    {
        [FunctionName("CreateTask")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Task")] HttpRequest req,
        [CosmosDB(
                DatabaseName,
                CollectionName,
                ConnectionStringSetting = "CosmosDBConnection")]
            IAsyncCollector<object> tasks, ILogger log)
        {
            log.LogInformation("Creating a new task list item.");
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
                task.CreatedTime,
                task.Category,
                task.TaskDescription,
                task.IsCompleted
            });

            log.LogInformation($"New Task created successfully with ID {task.Id}.");

            return new OkObjectResult(task);
        }
    }
}
