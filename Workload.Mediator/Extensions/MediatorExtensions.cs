namespace Workload.Mediator.Extensions;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

public static class MediatorExtensions
{
    public static IServiceCollection AddMediators(this IServiceCollection services)
    {
        _ = services.AddAutoMapper(typeof(MediatorExtensions).Assembly);
        _ = services.AddMediatR(typeof(MediatorExtensions).Assembly);

        return services;
    }
}
