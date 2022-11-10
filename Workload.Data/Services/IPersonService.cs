namespace Workload.Data.Services;

using Workload.Model;

public interface IPersonService
{
    Task<Person> CreatePerson(Person person);
}

