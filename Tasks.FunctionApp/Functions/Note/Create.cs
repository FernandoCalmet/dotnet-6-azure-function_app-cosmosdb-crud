using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Tasks.FunctionApp.Models;
using Tasks.FunctionApp.DataTransferObjects.Note;
using Tasks.FunctionApp.Exceptions;

namespace Tasks.FunctionApp.Functions.Note
{
    public class Create : Base
    {
        [FunctionName("CreateNote")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "notes")] HttpRequest req,
            [CosmosDB(
                DatabaseName,
                CollectionName,
                ConnectionStringSetting = "CosmosDBConnection",
                CreateIfNotExists = true)]
                IAsyncCollector<object> notes,
            ILogger log)
        {
            log.LogInformation("Creating a new note list item.");

            try
            {
                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<NoteForCreation>(requestBody);

                var note = new NoteModel()
                {
                    User = data.User,
                    Tasks = data.Tasks,
                };
                await notes.AddAsync(new
                {
                    id = note.Id,
                    user_id = note.User.Id,
                    created_time = note.CreatedTime,
                    tasks = note.Tasks,
                    user = note.User
                });

                log.LogInformation($"New note created successfully with ID {note.Id}.");

                return new OkObjectResult(note);
            }
            catch (NoteException exception)
            {
                log?.LogInformation(exception.ToString());
            }

            return new BadRequestResult();
        }
    }
}
