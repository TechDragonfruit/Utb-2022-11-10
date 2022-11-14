namespace Workload.Data.Services;

using Workload.Model;

public interface IPersonService
{
    Task<Person> CreatePerson(Person person);
    Task<Person> UpdatePerson(Person person);
    Task<Person> GetPerson(Guid id);
    Task<IEnumerable<Person>> GetPeople();
}

