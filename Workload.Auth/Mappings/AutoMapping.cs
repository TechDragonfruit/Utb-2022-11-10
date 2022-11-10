namespace EnergiBolaget.Auth.Mappings;

using AutoMapper;

using EnergiBolaget.Auth.Mediator;
using EnergiBolaget.Auth.Model;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        _ = CreateMap<RegisterUserRequest, User>()
            .ForMember(u => u.UserName, opt => opt.MapFrom(ur => ur.Email));

        _ = CreateMap<User, UserInfoResponse>()
            .ForCtorParam("Id", opt => opt.MapFrom(src => src.Id))
            .ForCtorParam("FirstName", opt => opt.MapFrom(src => src.FirstName))
            .ForCtorParam("LastName", opt => opt.MapFrom(src => src.LastName))
            .ForCtorParam("UserName", opt => opt.MapFrom(src => src.UserName))
            .ForCtorParam("NormalizedUserName", opt => opt.MapFrom(src => src.NormalizedUserName))
            .ForCtorParam("Email", opt => opt.MapFrom(src => src.Email))
            .ForCtorParam("NormalizedEmail", opt => opt.MapFrom(src => src.NormalizedEmail))
            .ForCtorParam("EmailConfirmed", opt => opt.MapFrom(src => src.EmailConfirmed))
            .ForCtorParam("PhoneNumber", opt => opt.MapFrom(src => src.PhoneNumber))
            .ForCtorParam("PhoneNumberConfirmed", opt => opt.MapFrom(src => src.PhoneNumberConfirmed))
            .ForCtorParam("TwoFactorEnabled", opt => opt.MapFrom(src => src.TwoFactorEnabled))
            .ForCtorParam("LockoutEnd", opt => opt.MapFrom(src => src.LockoutEnd))
            .ForCtorParam("LockoutEnabled", opt => opt.MapFrom(src => src.LockoutEnabled))
            .ForCtorParam("AccessFailedCount", opt => opt.MapFrom(src => src.AccessFailedCount));

        _ = CreateMap<CreateRoleRequest, Role>();
    }
}
