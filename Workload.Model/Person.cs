namespace Workload.Model;

public class Person
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;

    public ICollection<Workload> Workloads { get; set; } = new HashSet<Workload>();
}
