using App.EnglishBuddy.Domain.Entities;
using AutoMapper;

namespace App.EnglishBuddy.Application.Features.UserFeatures.GetAllUser;

public sealed class GetAllEmployeeMapper : Profile
{
    public GetAllEmployeeMapper()
    {
        //CreateMap<Users, GetAllEmployeeResponse>()
        // .ForMember(dest =>
        //    dest.Name,
        //    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
        // .ForMember(dest =>
        //    dest.Address,
        //    opt => opt.MapFrom(src => $"{src.CityName}, {src.StateName}, {"India"}"));
    }
}