using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Tasks.FunctionApp.Models;

namespace Tasks.FunctionApp.Functions.Task;

public class GetOneById : Base
{
    [FunctionName("GetOneTaskById")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Task/{id}")] HttpRequest req,
        [CosmosDB(
                DatabaseName,
                CollectionName,
                ConnectionStringSetting = "CosmosDBConnection",
                Id = "{id}")]
            TaskModel task,
        ILogger log, string id)
    {
        log.LogInformation("Getting task item by id.");

        if (task == null)
        {
            log.LogInformation($"Item {id} not found");
            return new NotFoundResult();
        }

        return new OkObjectResult(task);
    }
}
