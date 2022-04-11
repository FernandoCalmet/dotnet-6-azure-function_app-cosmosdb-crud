using System.IO;

namespace EfCoreFunctionApp.Functions;

public class UserFunctions
{
    private readonly IUserService _userService;
    private readonly IApiAuthentication _apiAuthentication;
    private const string Route = "users";

    public UserFunctions(IUserService userService, IApiAuthentication apiAuthentication)
    {
        _userService = userService;
        _apiAuthentication = apiAuthentication;
    }

    [FunctionName("CreateUser")]
    public async Task<IActionResult> Add(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = Route)]
            HttpRequest req, ILogger log)
    {
        try
        {
            var authResult = await _apiAuthentication.AuthenticateAsync(req.Headers);
            if (authResult.Failed) return new ForbidResult(authenticationScheme: "Bearer");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var command = JsonConvert.DeserializeObject<CreateUserCommand>(requestBody);
            await _userService.HandleCommandAsync(command);
            return new OkObjectResult(command);
        }
        catch (UserException exception)
        {
            log?.LogInformation(exception.ToString());
            return new BadRequestResult();
        }
    }

    [FunctionName("UpdateUserToken")]
    public async Task<IActionResult> UpdateUserToken(
        [HttpTrigger(AuthorizationLevel.Function, "put", Route = Route + "/usertoken")]
            HttpRequest req, ILogger log)
    {
        try
        {
            var authResult = await _apiAuthentication.AuthenticateAsync(req.Headers);
            if (authResult.Failed) return new ForbidResult(authenticationScheme: "Bearer");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var command = JsonConvert.DeserializeObject<SetUserTokenCommand>(requestBody);
            await _userService.HandleCommandAsync(command);
            return new OkObjectResult(command);
        }
        catch (UserException exception)
        {
            log?.LogInformation(exception.ToString());
            return new BadRequestResult();
        }
    }

    [FunctionName("GetAllUsers")]
    public async Task<IActionResult> GetAll(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = Route)]
            HttpRequest req, ILogger log)
    {
        try
        {
            var authResult = await _apiAuthentication.AuthenticateAsync(req.Headers);
            if (authResult.Failed) return new ForbidResult(authenticationScheme: "Bearer");

            var users = await _userService.HandleCommandAsync();
            return new OkObjectResult(users);
        }
        catch (UserException exception)
        {
            log?.LogInformation(exception.ToString());
        }

        return new BadRequestResult();
    }

    //[FunctionName("GetUserByGuid")]
    //public async Task<IActionResult> GetByGuid(
    //    [HttpTrigger(AuthorizationLevel.Function, "get", Route = Route + "/{guid}")]
    //        HttpRequest req, ILogger log, string guid)
    //{
    //    try
    //    {
    //        var authResult = await _apiAuthentication.AuthenticateAsync(req.Headers);
    //        if (authResult.Failed) return new ForbidResult(authenticationScheme: "Bearer");

    //        var user = await _userService.HandleCommandAsync(Guid.Parse(guid));
    //        if (user == null)
    //        {
    //            log.LogInformation($"El usuario con el {guid} no existe.");
    //            return new NotFoundResult();
    //        }
    //        return new OkObjectResult(user);
    //    }
    //    catch (UserException exception)
    //    {
    //        log?.LogInformation(exception.ToString());
    //    }

    //    return new BadRequestResult();
    //}

    //[FunctionName("DeleteUser")]
    //public async Task<IActionResult> Remove(
    //    [HttpTrigger(AuthorizationLevel.Function, "delete", Route = Route + "/{guid}")]
    //        HttpRequest req, ILogger log, string guid)
    //{
    //    try
    //    {
    //        var authResult = await _apiAuthentication.AuthenticateAsync(req.Headers);
    //        if (authResult.Failed) return new ForbidResult(authenticationScheme: "Bearer");

    //        var user = await _userService.HandleCommandAsync(Guid.Parse(guid));
    //        if (user == null)
    //        {
    //            log.LogInformation($"El usuario con el {guid} no existe.");
    //            return new NotFoundResult();
    //        }
    //        return new OkResult();
    //    }
    //    catch (UserException exception)
    //    {
    //        log?.LogInformation(exception.ToString());
    //    }

    //    return new BadRequestResult();
    //}
}
