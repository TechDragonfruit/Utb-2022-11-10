namespace Workload.Mediator.Mediators;

using AutoMapper;

using MediatR;

using Workload.Contract;
using Workload.Data.Exceptions;
using Workload.Data.Services;
using Workload.Model;

public class AssignmentMediator : MediatorBase, IRequestHandler<CreateAssignmentRequest, MediatorResponse>,
    IRequestHandler<GetAssignmentRequest, MediatorResponse>,
    IRequestHandler<GetAssignmentsRequest, MediatorResponse>
{
    private readonly IAssignmentService service;
    private readonly IMapper mapper;

    public AssignmentMediator(IAssignmentService service, IMapper mapper)
    {
        this.service = service;
        this.mapper = mapper;
    }

    public async Task<MediatorResponse> Handle(CreateAssignmentRequest request, CancellationToken cancellationToken)
    {
        try
        {
            Assignment assignment = await service.CreateAssignment(mapper.Map<Assignment>(request));
            return CreateResponse(200, "Assignment created", mapper.Map<CreateAssignmentResponse>(assignment));
        }
        catch (AssignmentAlreadyExistsException ex)
        {
            return CreateResponse(400, ex.Message, null);
        }
        catch (Exception ex)
        {
            return CreateResponse(500, ex.Message, null);
        }
    }

    public async Task<MediatorResponse> Handle(GetAssignmentRequest request, CancellationToken cancellationToken)
    {
        try
        {
            Assignment assignment = await service.GetAssignment(request.Id);
            return CreateResponse(200, "Assignment found", mapper.Map<GetAssignmentResponse>(assignment));
        }
        catch (AssignmentNotFoundException ex)
        {
            return CreateResponse(404, ex.Message, null);
        }
        catch (Exception ex)
        {
            return CreateResponse(500, ex.Message, null);
        }
    }

    public async Task<MediatorResponse> Handle(GetAssignmentsRequest request, CancellationToken cancellationToken)
    {
        try
        {
            IEnumerable<Assignment> assignment = await service.GetAssignments();
            return assignment.Any()
                ? CreateResponse(200, "Assignments found", assignment.Select(a => mapper.Map<GetAssignmentResponse>(a)))
                : CreateResponse(404, "Assignments not found", Enumerable.Empty<Assignment>());
        }
        catch (Exception ex)
        {
            return CreateResponse(500, ex.Message, null);
        }
    }
}



