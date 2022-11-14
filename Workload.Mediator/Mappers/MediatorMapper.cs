namespace Workload.Mediator.Mappers;

using AutoMapper;

using Workload.Contract;
using Workload.Model;

public class MediatorMapper : Profile
{
    public MediatorMapper()
    {
        //LENNART Fix mappings
        _ = CreateMap<CreatePersonRequest, Person>();
        _ = CreateMap<UpdatePersonRequest, Person>();
        _ = CreateMap<Person, PersonResponse>()
            .ForCtorParam("Id", p => p.MapFrom("Id"))
            .ForCtorParam("Name", p => p.MapFrom("Name"));

        _ = CreateMap<CreateAssignmentRequest, Assignment>();
        _ = CreateMap<UpdateAssignmentRequest, Assignment>();
        _ = CreateMap<Assignment, AssignmentResponse>()
            .ForCtorParam("Id", p => p.MapFrom("Id"))
            .ForCtorParam("Description", p => p.MapFrom("Description"));

        _ = CreateMap<CreateWorkloadRequest, Workload>();
        _ = CreateMap<UpdateWorkloadRequest, Workload>();
        _ = CreateMap<Workload, WorkloadResponse>()
            .ForCtorParam("Id", p => p.MapFrom("Id"));
    }
}



