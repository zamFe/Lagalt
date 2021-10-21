using AutoMapper;
using LagaltAPI.Models.Domain;
using LagaltAPI.Models.DTOs.User;

namespace LagaltAPI.Profiles
{
    public class UserProfile : Profile
    {
        // Constructor.
        public UserProfile()
        {
            CreateMap<User, UserCompleteReadDTO>();
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
