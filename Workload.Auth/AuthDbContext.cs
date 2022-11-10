namespace EnergiBolaget.Auth;

using EnergiBolaget.Auth.Model;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public interface IAuthDbContext
{
    DbSet<User> Users { get; }
    DbSet<Role> Roles { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    int SaveChanges();
}

public class AuthDbContext : IdentityDbContext<User, Role, Guid>, IAuthDbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options)
    { }

    public void EnsureDbExists()
    {
        IEnumerable<string> migrations = Database.GetPendingMigrations();
        if (migrations.Any())
        {
            Database.Migrate();
        }
        EnsureBaseRolesExists();
    }

    private void EnsureBaseRolesExists()
    {
        if (!Roles.Where(r => r.Name == "User").Any())
        {
            _ = Roles.Add(new Role { Id = Guid.NewGuid(), Name = "User", NormalizedName = "USER", ConcurrencyStamp = Guid.NewGuid().ToString() });
            try
            {
                _ = SaveChanges();
            }
            catch (Exception)
            {
                //TODO Add a logger
                //Console.WriteLine(ex.Message);
            }
        }

        if (!Roles.Where(r => r.Name == "Admin").Any())
        {
            _ = Roles.Add(new Role { Id = Guid.NewGuid(), Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = Guid.NewGuid().ToString() });
            _ = SaveChanges();
        }
    }
}
