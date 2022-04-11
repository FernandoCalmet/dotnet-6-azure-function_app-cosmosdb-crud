using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Tasks.FunctionApp.Models;
using Tasks.FunctionApp.DataTransferObjects.User;
using Tasks.FunctionApp.Exceptions;

namespace Tasks.FunctionApp.Functions.User
{
    public class Create : Base
    {
        [FunctionName("CreateUser")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "users")] HttpRequest req,
            [CosmosDB(
                DatabaseName,
                CollectionName,
                ConnectionStringSetting = "CosmosDBConnection",
                CreateIfNotExists = true)]
                IAsyncCollector<object> tasks,
            ILogger log)
        {
            log.LogInformation("Creating a new user list item.");

            try
            {
                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<UserForCreation>(requestBody);

                var user = new UserModel()
                {
                    FirstName = data.FirstName,
                    LastName = data.LastName
                };
                await tasks.AddAsync(new
                {
                    id = user.Id,
                    role = user.Role,
                    first_name = user.FirstName,
                    last_name = user.LastName
                });

                log.LogInformation($"New user created successfully with ID {user.Id}.");

                return new OkObjectResult(user);
            }
            catch (UserException exception)
            {
                log?.LogInformation(exception.ToString());
            }

            return new BadRequestResult();
        }
    }
}
