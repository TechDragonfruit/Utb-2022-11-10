namespace Workload.Contract;
using MediatR;

using System;

public record CreateAssignmentRequest(string Description) : IRequest<MediatorResponse>;
public record UpdateAssignmentRequest(Guid Id, string Description) : IRequest<MediatorResponse>;

public record CreatePersonRequest(string Name) : IRequest<MediatorResponse>;
public record UpdatePersonRequest(Guid Id, string Name) : IRequest<MediatorResponse>;

public record CreateWorkloadRequest(DateTimeOffset Start, Guid PersonId, Guid AssignmentId) : IRequest<MediatorResponse>;
public record UpdateWorkloadRequest(Guid Id, DateTimeOffset Start, Guid PersonId, Guid AssignmentId) : IRequest<MediatorResponse>;


