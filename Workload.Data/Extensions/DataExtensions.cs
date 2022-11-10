namespace Workload.Data.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Workload.Data.Services;

public static class DataExtensions
{
    public static IServiceCollection AddDataSupport(this IServiceCollection services, Func<string> getConnection)
    {
        string? connectionString = getConnection?.Invoke();

        if (connectionString == null) { throw new NotSupportedException("Ange connectionstring ditt pucko!!!"); };

        _ = services.AddTransient<IPersonService, WorkloadService>();
        _ = services.AddTransient<IAssignmentService, WorkloadService>();
        _ = services.AddTransient<IWorkloadService, WorkloadService>();

        _ = services.AddDbContext<IWorkloadDbContext, WorkloadDbContext>(options => options.UseSqlServer(connectionString!));

        return services;
    }
}

