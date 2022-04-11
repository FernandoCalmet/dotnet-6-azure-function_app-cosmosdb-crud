using EfCoreFunctionApp.Extensions;
using EfCoreFunctionApp.Infrastructure;
using EfCoreFunctionApp.Services;

[assembly: FunctionsStartup(typeof(FunctionApp.Identity.Startup))]
namespace FunctionApp.Identity;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        // Creating policies that wraps the authorization requirements
        builder.Services.AddOidcApiAuthorization();

        // CosmosDB Connection
        var config = new ConfigurationBuilder()
            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        string databaseName = config["DatabaseName"];
        string connString = config["CosmosDbConnectionString"];

        builder.Services.AddEntityDb(connString, databaseName);

        // Objects instances
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUserService, UserService>();
    }
}