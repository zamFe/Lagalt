using AutoMapper;
using LagaltAPI.Models.Domain;
using LagaltAPI.Models.DTOs.Project;

namespace LagaltAPI.Profiles
{
    public class ProjectProfile : Profile
    {
        // Constructor.
        public ProjectProfile()
        {
            CreateMap<Project, ProjectCompleteReadDTO>();
            CreateMap<Project, ProjectCompactReadDTO>();

            CreateMap<ProjectCreateDTO, Project>()
                .ForMember(p => p.Users, opt => opt
                .Ignore())
                .ForMember(p => p.Skills, opt => opt
                .Ignore());
            CreateMap<ProjectEditDTO, Project>()
                .ForMember(p => p.Skills, opt => opt
                .Ignore());
        }
    }
}
