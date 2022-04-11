namespace FunctionApp.Identity.Functions;

public class Info
{
    private readonly IApiAuthentication apiAuthentication;

    public Info(IApiAuthentication apiAuthentication)
    {
        this.apiAuthentication = apiAuthentication;
    }

    [FunctionName("Info")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        // Authenticate the user
        var authResult = await apiAuthentication.AuthenticateAsync(req.Headers);

        // Check the authentication result
        if (authResult.Failed)
        {
            return new ForbidResult(authenticationScheme: "Bearer");
        }

        string responseMessage = $"Healthy - {DateTime.Now.Year}";
        return new OkObjectResult(new[] {
                new {Message = responseMessage},
                new {Message = "La API con autenticación por AD-B2C esta funcionando correctamente."}
            });
    }
}
