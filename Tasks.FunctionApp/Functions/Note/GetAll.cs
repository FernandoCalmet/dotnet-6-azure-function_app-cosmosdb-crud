using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Tasks.FunctionApp.Models;
using Tasks.FunctionApp.Exceptions;

namespace Tasks.FunctionApp.Functions.Note;

public class GetAll : Base
{
    [FunctionName("GetAllNotes")]
    public static IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "notes")] HttpRequest req,
    [CosmosDB(
            DatabaseName,
            CollectionName,
            ConnectionStringSetting = "CosmosDBConnection",
            SqlQuery = "SELECT * FROM c order by c._ts desc")]
        IEnumerable<NoteModel> notes,
    ILogger log)
    {
        log.LogInformation("Getting note list items");

        try
        {
            return new OkObjectResult(notes);
        }
        catch (NoteException exception)
        {
            log?.LogInformation(exception.ToString());
        }

        return new BadRequestResult();
    }
}
