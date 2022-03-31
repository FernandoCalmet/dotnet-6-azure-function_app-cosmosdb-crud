using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Tasks.FunctionApp.Models;
using Tasks.FunctionApp.Exceptions;

namespace Tasks.FunctionApp.Functions.Task;

public class GetAll : Base
{
    [FunctionName("GetAllTasks")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "task")] HttpRequest req,
    [CosmosDB(
            DatabaseName,
            CollectionName,
            ConnectionStringSetting = "CosmosDBConnection",
            SqlQuery = "SELECT * FROM c order by c._ts desc")]
        IEnumerable<TaskModel> tasks,
    ILogger log)
    {
        log.LogInformation("Getting task list items");

        try
        {
            return new OkObjectResult(tasks);
        }
        catch (TaskException exception)
        {
            log?.LogInformation(exception.ToString());
        }

        return new BadRequestResult();
    }
}
