using AutoMapper;
using LagaltAPI.Models.Domain;
using LagaltAPI.Models.DTOs.Project;
using System.Linq;

namespace LagaltAPI.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectReadDTO>()
                .ForMember(pdto => pdto.Profession, opt => opt
                .MapFrom(p => p.ProfessionId))
                .ForMember(pdto => pdto.Users, opt => opt
                .MapFrom(p => p.UserProjects.Select(up => up.UserID).ToList()))
                .ForMember(pdto => pdto.Skills, opt => opt
                .MapFrom(p => p.Skills.ToList()))
                .ReverseMap();

            CreateMap<Project, ProjectEditDTO>()
                .ForMember(pdto => pdto.Users, opt => opt
                .MapFrom(p => p.UserProjects.Select(up => up.UserID).ToList()))
                .ForMember(pdto => pdto.Skills, opt => opt
                .MapFrom(p => p.Skills.Select(s => s.Id).ToList()))
                .ReverseMap();

            CreateMap<Project, ProjectCreateDTO>()
                .ForMember(pdto => pdto.Users, opt => opt
                .MapFrom(p => p.UserProjects.Select(up => up.UserID).ToList()))
                .ForMember(pdto => pdto.Skills, opt => opt
                .MapFrom(p => p.Skills.Select(s => s.Id).ToList()))
                .ReverseMap();
        }
    }
}
