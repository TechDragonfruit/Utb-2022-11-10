namespace Workload.Contract;

public record AssignmentResponse(Guid Id, string Description);
public record PersonResponse(Guid Id, string Name);
public record WorkloadResponse(Guid Id, DateTimeOffset Start, DateTimeOffset Stop, Guid PersonId, Guid AssignmentId);
