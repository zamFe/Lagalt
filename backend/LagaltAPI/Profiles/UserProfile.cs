using AutoMapper;
using LagaltAPI.Models.Domain;
using LagaltAPI.Models.DTOs.User;
using System.Linq;

namespace LagaltAPI.Profiles
{
    public class UserProfile : Profile
    {
        // Constructor.
        public UserProfile()
        {
            CreateMap<User, UserCompleteReadDTO>()
                .ForMember(udto => udto.Skills, opt => opt
                .MapFrom(u => u.Skills.ToList()))
                .ReverseMap();
            CreateMap<User, UserCompactReadDTO>();

            CreateMap<UserCreateDTO, User>()
                .ForMember(udto => udto.Skills, opt => opt
                .Ignore());
            CreateMap<UserEditDTO, User>()
            .ForMember(udto => udto.Skills, opt => opt
                .Ignore());
        }
    }
}
