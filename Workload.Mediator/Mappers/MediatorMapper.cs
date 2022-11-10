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
        _ = CreateMap<Person, CreatePersonResponse>()
            .ForCtorParam("Id", p => p.MapFrom("Id"))
            .ForCtorParam("Name", p => p.MapFrom("Name"));

        _ = CreateMap<CreateAssignmentRequest, Assignment>();
        _ = CreateMap<Assignment, CreateAssignmentResponse>()
            .ForCtorParam("Id", p => p.MapFrom("Id"))
            .ForCtorParam("Description", p => p.MapFrom("Description"));
        _ = CreateMap<Assignment, GetAssignmentResponse>()
            .ForCtorParam("Id", p => p.MapFrom("Id"))
            .ForCtorParam("Description", p => p.MapFrom("Description"));

        _ = CreateMap<CreateWorkloadRequest, Workload>();
        _ = CreateMap<Workload, CreateWorkloadResponse>()
            .ForCtorParam("Id", p => p.MapFrom("Id"));
    }
}



