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
using Tasks.FunctionApp.DataTransferObjects.Task;
using System.Linq;
using Tasks.FunctionApp.Models;
using Tasks.FunctionApp.Exceptions;

namespace Tasks.FunctionApp.Functions.Task;

public class Update : Base
{
    [FunctionName("UpdateTask")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "tasks/{id}")]
            HttpRequest req,
        [CosmosDB(ConnectionStringSetting = "CosmosDBConnection")]
            DocumentClient client,
        ILogger log,
        string id)
    {
        log.LogInformation("Updating a task list item.");

        try
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var updated = JsonConvert.DeserializeObject<TaskForUpdate>(requestBody);
            Uri collectionUri = UriFactory.CreateDocumentCollectionUri(DatabaseName, CollectionName);
            var document = client.CreateDocumentQuery(collectionUri).Where(t => t.Id == id)
                            .AsEnumerable().FirstOrDefault();

            if (document == null) return new NotFoundResult();

            if (!string.IsNullOrEmpty(updated.Category)) document.SetPropertyValue("Category", updated.Category);

            if (!string.IsNullOrEmpty(updated.TaskDescription)) document.SetPropertyValue("TaskDescription", updated.TaskDescription);

            document.SetPropertyValue("IsCompleted", updated.IsCompleted);
            await client.ReplaceDocumentAsync(document);
            TaskModel task = (dynamic)document;
            log.LogInformation($"New Task updated successfully with ID {task.Id}.");

            return new OkObjectResult(task);
        }
        catch (TaskException exception)
        {
            log?.LogInformation(exception.ToString());
        }

        return new BadRequestResult();
    }
}
