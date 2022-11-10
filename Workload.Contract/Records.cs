namespace Workload.Contract;

using MediatR;

public record CreatePersonRequest(string Name) : IRequest<MediatorResponse>;
public record CreateAssignmentRequest(string Description) : IRequest<MediatorResponse>;
public record GetAssignmentRequest(Guid Id) : IRequest<MediatorResponse>;
public record GetAssignmentsRequest() : IRequest<MediatorResponse>;

public record CreateWorkloadRequest(DateTimeOffset Start, Guid PersonId, Guid AssignmentId) : IRequest<MediatorResponse>;
public record CreatePersonResponse(Guid Id, string Name);
public record CreateAssignmentResponse(Guid Id, string Description);
public record GetAssignmentResponse(Guid Id, string Description);
public record CreateWorkloadResponse(Guid Id);
