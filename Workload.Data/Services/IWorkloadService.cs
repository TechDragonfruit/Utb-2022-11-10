namespace Workload.Data.Services;

using Workload.Model;

public interface IWorkloadService
{
    Task<Workload> CreateWorkload(Workload workload);
    Task<Workload> UpdateWorkload(Workload workload);
    Task<Workload> GetWorkload(Guid id);
    Task<IEnumerable<Workload>> GetWorkloads();
}

