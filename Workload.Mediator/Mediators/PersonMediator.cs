namespace Workload.Mediator.Mediators;

using AutoMapper;

using MediatR;

using Workload.Contract;
using Workload.Data.Exceptions;
using Workload.Data.Services;
using Workload.Model;

public class PersonMediator : MediatorBase,
    IRequestHandler<CreatePersonRequest, MediatorResponse>,
    IRequestHandler<UpdatePersonRequest, MediatorResponse>,
    IRequestHandler<GetPersonRequest, MediatorResponse>,
    IRequestHandler<GetPeopleRequest, MediatorResponse>

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
            return CreateResponse(200, "Person created", mapper.Map<PersonResponse>(person));
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

    public async Task<MediatorResponse> Handle(UpdatePersonRequest request, CancellationToken cancellationToken)
    {
        try
        {
            Person person = await service.UpdatePerson(mapper.Map<Person>(request));
            return CreateResponse(200, "Person updated", mapper.Map<PersonResponse>(person));
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

    public async Task<MediatorResponse> Handle(GetPersonRequest request, CancellationToken cancellationToken)
    {
        try
        {
            Person person = await service.GetPerson(request.Id);
            return CreateResponse(200, "Person found", mapper.Map<PersonResponse>(person));
        }
        catch (PersonNotFoundException ex)
        {
            return CreateResponse(404, ex.Message, null);
        }
        catch (Exception ex)
        {
            return CreateResponse(500, ex.Message, null);
        }
    }

    public async Task<MediatorResponse> Handle(GetPeopleRequest request, CancellationToken cancellationToken)
    {
        try
        {
            IEnumerable<Person> people = await service.GetPeople();
            return people.Any()
                ? CreateResponse(200, "People found", people.Select(a => mapper.Map<PersonResponse>(a)))
                : CreateResponse(404, "Assignments not found", Enumerable.Empty<Person>());
        }
        catch (Exception ex)
        {
            return CreateResponse(500, ex.Message, null);
        }
    }
}




