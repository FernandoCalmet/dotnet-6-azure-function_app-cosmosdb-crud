using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Tasks.FunctionApp.Models;
using System;
using Microsoft.Azure.Documents.Client;
using System.Linq;
using Tasks.FunctionApp.Exceptions;

namespace Tasks.FunctionApp.Functions.User;

public class GetOneById : Base
{
    [FunctionName("GetOneUserById")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users/{id}")] HttpRequest req,
        [CosmosDB(
            DatabaseName,
            CollectionName,
            ConnectionStringSetting = "CosmosDBConnection",
            Id = "{id}")]
                DocumentClient client,
            ILogger log, string id)
    {
        log.LogInformation("Getting user item by id.");

        try
        {
            Uri collectionUri = UriFactory.CreateDocumentCollectionUri(DatabaseName, CollectionName);
            var document = client.CreateDocumentQuery(collectionUri).Where(t => t.Id == id)
                            .AsEnumerable().FirstOrDefault();
            if (document == null)
            {
                log.LogInformation($"Item {id} not found");
                return new NotFoundResult();
            }

            await client.ReplaceDocumentAsync(document);
            UserModel user = (dynamic)document;

            return new OkObjectResult(user);
        }
        catch (UserException exception)
        {
            log?.LogInformation(exception.ToString());
        }

        return new BadRequestResult();
    }
}
