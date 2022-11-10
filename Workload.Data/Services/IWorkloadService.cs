namespace Workload.Data.Services;

using Workload.Model;

public interface IWorkloadService
{
    Task<Workload> CreateWorkload(Workload workload);
}

