namespace Workload.Model;

public class Assignment
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Description { get; set; } = string.Empty;

    public ICollection<Workload> Workloads { get; set; } = new HashSet<Workload>();
}
