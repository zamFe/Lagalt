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
            CreateMap<Project, ProjectCompleteReadDTO>();
            CreateMap<Project, ProjectCompactReadDTO>();
            CreateMap<Project, ProjectEditDTO>();

            CreateMap<ProjectCreateDTO, Project>();
        }
    }
}
