namespace Workload.Mediator.Mediators;

using AutoMapper;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

using Workload.Contract;
using Workload.Data.Exceptions;
using Workload.Data.Services;
using Workload.Model;

public class WorkloadMediator : MediatorBase, IRequestHandler<CreateWorkloadRequest, MediatorResponse>
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
            return CreateResponse(200, "Workload created", mapper.Map<CreateWorkloadResponse>(workload));
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
}



