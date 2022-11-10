namespace Workload.Mediator.Mediators;

using AutoMapper;

using MediatR;

using Workload.Contract;
using Workload.Data.Exceptions;
using Workload.Data.Services;
using Workload.Model;

public class PersonMediator : MediatorBase, IRequestHandler<CreatePersonRequest, MediatorResponse>

{
    private readonly IPersonService service;
    private readonly IMapper mapper;

    public PersonMediator(IPersonService service, IMapper mapper)
    {
        this.service = service;
        this.mapper = mapper;
    }

    public async Task<MediatorResponse> Handle(CreatePersonRequest request, CancellationToken cancellationToken)
    {
        try
        {
            Person person = await service.CreatePerson(mapper.Map<Person>(request));
            return CreateResponse(200, "Person created", mapper.Map<CreatePersonResponse>(person));
        }
        catch (PersonAlreadyExistsException ex)
        {
            return CreateResponse(400, ex.Message, null);
        }
        catch (Exception ex)
        {
            return CreateResponse(500, ex.Message, null);
        }
    }

}



