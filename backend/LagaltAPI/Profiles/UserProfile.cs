﻿using AutoMapper;
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
            CreateMap<User, UserReadDTO>()
                .ForMember(udto => udto.Skills, opt => opt
                .MapFrom(u => u.Skills.ToList()))
                .ForMember(udto => udto.Projects, opt => opt
                .MapFrom(u => u.UserProjects.Select(up => up.ProjectID).ToList()))
                .ReverseMap();

            CreateMap<User, UserEditDTO>()
                .ForMember(udto => udto.Skills, opt => opt
                .MapFrom(u => u.Skills.Select(s => s.Id).ToList()))
                .ReverseMap();

            CreateMap<User, UserCreateDTO>()
                .ForMember(udto => udto.Skills, opt => opt
                .MapFrom(u => u.Skills.Select(s => s.Id).ToList()))
                .ReverseMap();
        }
    }
}
