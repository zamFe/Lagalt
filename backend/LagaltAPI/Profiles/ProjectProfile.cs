using AutoMapper;
using LagaltAPI.Models;
using LagaltAPI.Models.DTOs.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LagaltAPI.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectReadDTO>()
                .ForMember(pdto => pdto.Profession, opt => opt
                .MapFrom(p => p.ProfessionId))
                .ForMember(pdto => pdto.Messages, opt => opt
                .MapFrom(p => p.Messages.Select(m => m.Id).ToList()))
                .ForMember(pdto => pdto.Users, opt => opt
                .MapFrom(p => p.UserProjects.Select(up => up.UserID).ToList()))
                .ReverseMap();

            CreateMap<Project, ProjectEditDTO>()
                .ForMember(pdto => pdto.Messages, opt => opt
                .MapFrom(p => p.Messages.Select(m => m.Id).ToList()))
                .ForMember(pdto => pdto.Users, opt => opt
                .MapFrom(p => p.UserProjects.Select(up => up.UserID).ToList()))
                .ReverseMap();

            CreateMap<Project, ProjectCreateDTO>()
                .ForMember(pdto => pdto.Messages, opt => opt
                .MapFrom(p => p.Messages.Select(m => m.Id).ToList()))
                .ForMember(pdto => pdto.Users, opt => opt
                .MapFrom(p => p.UserProjects.Select(up => up.UserID).ToList()))
                .ReverseMap();
        }
    }
}
