namespace Workload.Data;

using Microsoft.EntityFrameworkCore;

using Workload.Model;

public class WorkloadDbContext : DbContext, IWorkloadDbContext
{
    public DbSet<Person> People => Set<Person>();
    public DbSet<Assignment> Assignments => Set<Assignment>();
    public DbSet<Workload> Workloads => Set<Workload>();

    public WorkloadDbContext(DbContextOptions<WorkloadDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.ApplyConfigurationsFromAssembly(typeof(WorkloadDbContext).Assembly);
    }

    public Task EnsureDbExists()
    {
        if (Database.GetPendingMigrations().Any())
        {
            Database.Migrate();
        }

        return Task.CompletedTask;
    }
}
