using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Tasks.FunctionApp.Models;
using Tasks.FunctionApp.Exceptions;

namespace Tasks.FunctionApp.Functions.User;

public class GetAll : Base
{
    [FunctionName("GetAllUsers")]
    public static IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users")] HttpRequest req,
    [CosmosDB(
            DatabaseName,
            CollectionName,
            ConnectionStringSetting = "CosmosDBConnection",
            SqlQuery = "SELECT * FROM c order by c._ts desc")]
        IEnumerable<UserModel> users,
    ILogger log)
    {
        log.LogInformation("Getting user list items");

        try
        {
            return new OkObjectResult(users);
        }
        catch (UserException exception)
        {
            log?.LogInformation(exception.ToString());
        }

        return new BadRequestResult();
    }
}
