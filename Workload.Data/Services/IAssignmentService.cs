namespace Workload.Data.Services;

using Workload.Model;

public interface IAssignmentService
{
    Task<Assignment> CreateAssignment(Assignment assignment);
    Task<Assignment> GetAssignment(Guid id);
    Task<IEnumerable<Assignment>> GetAssignments();
}

