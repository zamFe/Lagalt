using AutoMapper;
using LagaltAPI.Models.Domain;
using LagaltAPI.Models.DTOs.Application;

namespace LagaltAPI.Profiles
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Application, ApplicationReadDTO>();
            CreateMap<ApplicationCreateDTO, Application>();
        }
    }
}
