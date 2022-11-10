namespace Workload.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class DesignTimeWorkloadDbContext : IDesignTimeDbContextFactory<WorkloadDbContext>
{
    private static readonly string connectionString =
        @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=workloadsdb99;Integrated Security=True";
    public WorkloadDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<WorkloadDbContext> optionsBuilder = new();
        _ = optionsBuilder.UseSqlServer(connectionString);

        return new WorkloadDbContext(optionsBuilder.Options);
    }
}
