using EfCoreFunctionApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreFunctionApp.Infrastructure;

public class UserDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public UserDbContext(DbContextOptions<UserDbContext> optionsBuilder) : base(optionsBuilder)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region DefaultContainer
        modelBuilder.HasDefaultContainer("Identities");
        #endregion

        #region Container
        modelBuilder.Entity<User>()
            .ToContainer("Users");
        #endregion

        #region NoDiscriminator
        modelBuilder.Entity<User>()
            .HasNoDiscriminator();
        #endregion

        #region PartitionKey
        modelBuilder.Entity<User>()
            .HasPartitionKey(u => u.PartitionKey);
        #endregion

        #region HasKey
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        #endregion

        #region OwnsOne
        modelBuilder.Entity<User>().OwnsOne(u => u.UserToken);
        modelBuilder.Entity<User>().OwnsOne(u => u.UserName);
        modelBuilder.Entity<User>().OwnsOne(u => u.UserPassword);
        modelBuilder.Entity<User>().OwnsOne(u => u.UserContact);
        #endregion
    }
}
