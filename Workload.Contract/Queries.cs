namespace Workload.Contract;

using MediatR;

public record GetAssignmentRequest(Guid Id) : IRequest<MediatorResponse> { public static GetAssignmentRequest Instance(Guid id) { return new(id); } };
public record GetAssignmentsRequest() : IRequest<MediatorResponse> { public static GetAssignmentsRequest Instance => new(); };

public record GetPersonRequest(Guid Id) : IRequest<MediatorResponse> { public static GetPersonRequest Instance(Guid id) { return new(id); } };
public record GetPeopleRequest() : IRequest<MediatorResponse> { public static GetPeopleRequest Instance => new(); };

public record GetWorkloadRequest(Guid Id) : IRequest<MediatorResponse> { public static GetWorkloadRequest Instance(Guid id) { return new(id); } };
public record GetWorkloadsRequest() : IRequest<MediatorResponse> { public static GetWorkloadsRequest Instance => new(); };

