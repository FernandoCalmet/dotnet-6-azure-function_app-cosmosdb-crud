using EFCoreFunctionApp.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EfCoreFunctionApp.Extensions;

public static class IdentityDbContextExtensions
{
    public static void AddEntityDb(this IServiceCollection services, string connString, string databaseName)
    {
        services.AddDbContext<UserDbContext>(options =>
        {
            options.UseCosmos(connString, databaseName);
        });
    }
}
