namespace Workload.Data.Services;

using Workload.Data.Exceptions;
using Workload.Model;

public class WorkloadService : IAssignmentService, IPersonService, IWorkloadService
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

    public async Task<Assignment> UpdateAssignment(Assignment assignment)
    {
        if (assignment.Id == Guid.Empty)
        {
            throw new InvalidAssignmentException("Id must be set");
        }

        if (!context.Assignments.Any(a => a.Id == assignment.Id))
        {
            throw new AssignmentNotFoundException("Assignment not found");
        }

        _ = context.Assignments.Update(assignment);
        _ = await context.SaveChangesAsync();

        return assignment;
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

    public async Task<Person> CreatePerson(Person person)
    {
        if (person.Id == Guid.Empty)
        {
            throw new InvalidPersonException("Id must be set");
        }

        _ = context.People.Add(person);
        _ = await context.SaveChangesAsync();

        return person;
    }

    public async Task<Person> UpdatePerson(Person person)
    {
        if (person.Id == Guid.Empty)
        {
            throw new InvalidPersonException("Id must be set");
        }

        if (!context.People.Any(a => a.Id == person.Id))
        {
            throw new AssignmentNotFoundException("People not found");
        }

        _ = context.People.Update(person);
        _ = await context.SaveChangesAsync();

        return person;
    }

    public Task<Person> GetPerson(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new InvalidPersonException("Id must be set");
        }

        Person? person = context.People.SingleOrDefault(a => a.Id == id);
        return person == null ? throw new PersonNotFoundException("Not found") : Task.FromResult(person);
    }

    public Task<IEnumerable<Person>> GetPeople()
    {
        return Task.FromResult(context.People.AsEnumerable());
    }

    public async Task<Workload> CreateWorkload(Workload workload)
    {
        if (workload.Id == Guid.Empty)
        {
            throw new InvalidWorkloadException("Id must be set");
        }

        _ = context.Workloads.Add(workload);
        _ = await context.SaveChangesAsync();

        return workload;
    }

    public async Task<Workload> UpdateWorkload(Workload workload)
    {
        if (workload.Id == Guid.Empty)
        {
            throw new InvalidWorkloadException("Id must be set");
        }

        if (!context.Workloads.Any(a => a.Id == workload.Id))
        {
            throw new WorkloadNotFoundException("Assignment not found");
        }

        _ = context.Workloads.Update(workload);
        _ = await context.SaveChangesAsync();

        return workload;
    }

    public Task<Workload> GetWorkload(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new InvalidWorkloadException("Id must be set");
        }

        Workload? workload = context.Workloads.SingleOrDefault(a => a.Id == id);
        return workload == null ? throw new WorkloadNotFoundException("Not found") : Task.FromResult(workload);
    }

    public Task<IEnumerable<Workload>> GetWorkloads()
    {
        return Task.FromResult(context.Workloads.AsEnumerable());
    }
}


