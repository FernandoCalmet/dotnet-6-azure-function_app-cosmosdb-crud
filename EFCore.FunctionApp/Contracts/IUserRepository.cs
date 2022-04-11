using EfCoreFunctionApp.Entities;

namespace EfCoreFunctionApp.Contracts;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task<User> GetAsync(UserId userId);
    Task<IEnumerable<User>> GetAllAsync();
    Task RemoveAsync(User user);
}
