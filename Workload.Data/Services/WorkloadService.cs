namespace Workload.Data.Services;

using Workload.Data.Exceptions;
using Workload.Model;

public class WorkloadService : IPersonService, IAssignmentService, IWorkloadService
{
    private readonly IWorkloadDbContext context;

    public WorkloadService(IWorkloadDbContext context)
    {
        this.context = context;
    }

    public async Task<Assignment> CreateAssignment(Assignment assignment)
    {
        if (assignment.Id == Guid.Empty)
        {
            throw new InvalidAssignmentException("Id must be set");
        }

        _ = context.Assignments.Add(assignment);
        _ = await context.SaveChangesAsync();

        return assignment;
    }

    public Task<Person> CreatePerson(Person person)
    {
        throw new PersonAlreadyExistsException("Person already exists");
    }

    public Task<Workload> CreateWorkload(Workload workload)
    {
        throw new WorkloadAlreadyExistsException("Workload already exists");
    }

    public Task<Assignment> GetAssignment(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new InvalidAssignmentException("Id must be set");
        }

        Assignment? assignment = context.Assignments.SingleOrDefault(a => a.Id == id);
        return assignment == null ? throw new AssignmentNotFoundException("Not found") : Task.FromResult(assignment);
    }

    public Task<IEnumerable<Assignment>> GetAssignments()
    {
        return Task.FromResult(context.Assignments.AsEnumerable());
    }
}

