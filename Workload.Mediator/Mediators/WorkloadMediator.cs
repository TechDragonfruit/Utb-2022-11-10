namespace Workload.Mediator.Mediators;

using AutoMapper;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

using Workload.Contract;
using Workload.Data.Exceptions;
using Workload.Data.Services;
using Workload.Model;

public class WorkloadMediator : MediatorBase,
    IRequestHandler<CreateWorkloadRequest, MediatorResponse>,
    IRequestHandler<UpdateWorkloadRequest, MediatorResponse>,
    IRequestHandler<GetWorkloadRequest, MediatorResponse>,
    IRequestHandler<GetWorkloadsRequest, MediatorResponse>
{
    private readonly IWorkloadService service;
    private readonly IMapper mapper;

    public WorkloadMediator(IWorkloadService service, IMapper mapper)
    {
        this.service = service;
        this.mapper = mapper;
    }

    public async Task<MediatorResponse> Handle(CreateWorkloadRequest request, CancellationToken cancellationToken)
    {
        try
        {
            Workload workload = await service.CreateWorkload(mapper.Map<Workload>(request));
            return CreateResponse(200, "Workload created", mapper.Map<WorkloadResponse>(workload));
        }
        catch (WorkloadAlreadyExistsException ex)
        {
            return CreateResponse(400, ex.Message, null);
        }
        catch (Exception ex)
        {
            return CreateResponse(500, ex.Message, null);
        }
    }

    public async Task<MediatorResponse> Handle(UpdateWorkloadRequest request, CancellationToken cancellationToken)
    {
        try
        {
            Workload workload = await service.UpdateWorkload(mapper.Map<Workload>(request));
            return CreateResponse(200, "Workload updated", mapper.Map<WorkloadResponse>(workload));
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

    public async Task<MediatorResponse> Handle(GetWorkloadRequest request, CancellationToken cancellationToken)
    {
        try
        {
            Workload workload = await service.GetWorkload(request.Id);
            return CreateResponse(200, "Workload found", mapper.Map<WorkloadResponse>(workload));
        }
        catch (WorkloadNotFoundException ex)
        {
            return CreateResponse(404, ex.Message, null);
        }
        catch (Exception ex)
        {
            return CreateResponse(500, ex.Message, null);
        }
    }

    public async Task<MediatorResponse> Handle(GetWorkloadsRequest request, CancellationToken cancellationToken)
    {
        try
        {
            IEnumerable<Workload> workloads = await service.GetWorkloads();
            return workloads.Any()
                ? CreateResponse(200, "Workloads found", workloads.Select(a => mapper.Map<WorkloadResponse>(a)))
                : CreateResponse(404, "Workloads not found", Enumerable.Empty<Workload>());
        }
        catch (Exception ex)
        {
            return CreateResponse(500, ex.Message, null);
        }
    }
}




