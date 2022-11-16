namespace Workload.Model;

public class Workload
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTimeOffset Start { get; set; }
    public DateTimeOffset? Stop { get; set; }

    public Guid PersonId { get; set; }
    public Guid AssignmentId { get; set; }

    public Person Person { get; set; }
    public Assignment Assignment { get; set; }
}
