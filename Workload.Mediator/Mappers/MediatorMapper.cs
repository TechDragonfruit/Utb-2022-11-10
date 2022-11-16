namespace Workload.Mediator.Mappers;

using AutoMapper;

using Workload.Contract;
using Workload.Model;

public class MediatorMapper : Profile
{
    public MediatorMapper()
    {
        //TODO Mapping of relations
        _ = CreateMap<CreatePersonRequest, Person>();
        _ = CreateMap<UpdatePersonRequest, Person>();
        _ = CreateMap<Person, PersonResponse>();

        _ = CreateMap<CreateAssignmentRequest, Assignment>();
        _ = CreateMap<UpdateAssignmentRequest, Assignment>();
        _ = CreateMap<Assignment, AssignmentResponse>();

        _ = CreateMap<CreateWorkloadRequest, Workload>();
        _ = CreateMap<UpdateWorkloadRequest, Workload>();
        _ = CreateMap<Workload, WorkloadResponse>();
    }
}



