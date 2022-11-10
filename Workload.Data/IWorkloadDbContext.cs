namespace Workload.Data;

using Microsoft.EntityFrameworkCore;

using Workload.Model;

public interface IWorkloadDbContext
{
    DbSet<Assignment> Assignments { get; }
    DbSet<Person> People { get; }
    DbSet<Workload> Workloads { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    Task EnsureDbExists();
}
