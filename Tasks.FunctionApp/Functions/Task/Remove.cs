using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Documents.Client;
using System.Linq;

namespace Tasks.FunctionApp.Functions.Task;

public class Remove : Base
{
    [FunctionName("RemoveTask")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "Task/{id}")]
            HttpRequest req,
        [CosmosDB(ConnectionStringSetting = "CosmosDBConnection")]
            DocumentClient client,
        ILogger log,
        string id)
    {
        log.LogInformation("Deleting a task from list item.");

        Uri collectionUri = UriFactory.CreateDocumentCollectionUri(DatabaseName, CollectionName);
        var document = client.CreateDocumentQuery(collectionUri).Where(t => t.Id == id)
                .AsEnumerable().FirstOrDefault();
        if (document == null)
        {
            return new NotFoundResult();
        }

        await client.DeleteDocumentAsync(document.SelfLink);
        log.LogInformation($"Task deleted successfully with ID {id}.");

        return new OkResult();
    }
}
