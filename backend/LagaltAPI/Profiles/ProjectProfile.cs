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
            CreateMap<Project, ProjectEditDTO>();

            CreateMap<ProjectCreateDTO, Project>();
        }
    }
}
