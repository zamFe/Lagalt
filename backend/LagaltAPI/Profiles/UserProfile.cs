using AutoMapper;
using LagaltAPI.Models;
using LagaltAPI.Models.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LagaltAPI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserReadDTO>()
                .ForMember(udto => udto.Skills, opt => opt
                .MapFrom(u => u.Skills.Select(s => s.Id).ToList()))
                .ForMember(udto => udto.Projects, opt => opt
                .MapFrom(u => u.UserProjects.Select(up => up.ProjectID).ToList()))
                .ForMember(udto => udto.Messages, opt => opt
                .MapFrom(u => u.Messages.Select(m => m.Id).ToList()))
                .ReverseMap();

            CreateMap<User, UserEditDTO>()
                .ForMember(udto => udto.Skills, opt => opt
                .MapFrom(u => u.Skills.Select(s => s.Id).ToList()))
                .ForMember(udto => udto.Projects, opt => opt
                .MapFrom(u => u.UserProjects.Select(up => up.ProjectID).ToList()))
                .ForMember(udto => udto.Messages, opt => opt
                .MapFrom(u => u.Messages.Select(m => m.Id).ToList()))
                .ReverseMap();

            CreateMap<User, UserCreateDTO>()
                .ForMember(udto => udto.Skills, opt => opt
                .MapFrom(u => u.Skills.Select(s => s.Id).ToList()))
                .ForMember(udto => udto.Projects, opt => opt
                .MapFrom(u => u.UserProjects.Select(up => up.ProjectID).ToList()))
                .ForMember(udto => udto.Messages, opt => opt
                .MapFrom(u => u.Messages.Select(m => m.Id).ToList()))
                .ReverseMap();
        }
    }
}
