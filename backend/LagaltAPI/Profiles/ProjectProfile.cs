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
                .ForMember(pdto => pdto.Skills, opt => opt
                .MapFrom(p => p.Skills.ToList()))
                .ReverseMap();

            CreateMap<Project, ProjectEditDTO>()
                .ForMember(pdto => pdto.Profession, opt => opt
                .MapFrom(p => p.Profession.Id))
                .ForMember(pdto => pdto.Users, opt => opt
                .MapFrom(p => p.Users.Select(up => up.Id).ToList()))
                .ForMember(pdto => pdto.Skills, opt => opt
                .MapFrom(p => p.Skills.Select(s => s.Id).ToList()))
                .ReverseMap();

            CreateMap<ProjectCreateDTO, Project>()
                .ForMember(p => p.ProfessionId, opt => opt
                .MapFrom(pdto => pdto.Profession))
                .ForMember(p => p.Profession, opt => opt
                .Ignore())
                .ForMember(p => p.Users, opt => opt // not working
                .MapFrom(pdto => pdto.Users)) // not working
                .ForMember(p => p.Skills, opt => opt // not working
                .MapFrom(pdto => pdto.Skills)); // not working
        }
    }
}
