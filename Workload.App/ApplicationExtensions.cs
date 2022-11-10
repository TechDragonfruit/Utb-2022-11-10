namespace Workload.App;

using Microsoft.Extensions.DependencyInjection;

using Workload.Data.Extensions;
using Workload.Mediator.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddAppPart(this IServiceCollection services, Func<string> getConnection)
    {
        _ = services.AddDataSupport(getConnection);
        _ = services.AddMediators();
        _ = services.AddControllers().AddApplicationPart(typeof(ApplicationExtensions).Assembly);

        return services;
    }
}
