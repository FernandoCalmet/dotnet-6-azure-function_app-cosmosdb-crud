using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Documents.Client;
using Tasks.FunctionApp.DataTransferObjects.User;
using System.Linq;
using Tasks.FunctionApp.Models;
using Tasks.FunctionApp.Exceptions;

namespace Tasks.FunctionApp.Functions.User;

public class Update : Base
{
    [FunctionName("UpdateUser")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "users/{id}")]
            HttpRequest req,
        [CosmosDB(ConnectionStringSetting = "CosmosDBConnection")]
            DocumentClient client,
        ILogger log,
        string id)
    {
        log.LogInformation("Updating a user list item.");

        try
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var updated = JsonConvert.DeserializeObject<UserForUpdate>(requestBody);
            Uri collectionUri = UriFactory.CreateDocumentCollectionUri(DatabaseName, CollectionName);
            var document = client.CreateDocumentQuery(collectionUri).Where(t => t.Id == id)
                            .AsEnumerable().FirstOrDefault();

            if (document == null) return new NotFoundResult();

            if (!string.IsNullOrEmpty(updated.FirstName)) document.SetPropertyValue("FirstName", updated.FirstName);

            if (!string.IsNullOrEmpty(updated.LastName)) document.SetPropertyValue("LastName", updated.LastName);

            await client.ReplaceDocumentAsync(document);

            UserModel user = (dynamic)document;
            log.LogInformation($"New user updated successfully with ID {user.Id}.");

            return new OkObjectResult(user);
        }
        catch (UserException exception)
        {
            log?.LogInformation(exception.ToString());
        }

        return new BadRequestResult();
    }
}
