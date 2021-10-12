using AutoMapper;
using LagaltAPI.Models;
using LagaltAPI.Models.DTOs.Skill;
using System.Linq;

namespace LagaltAPI.Profiles
{
    public class SkillProfile : Profile
    {
        public SkillProfile()
        {
            CreateMap<Skill, SkillCreateDTO>()
                .ForMember(sdto => sdto.Users, opt => opt
                .MapFrom(s => s.Users.Select(u => u.Id).ToList()))
                .ReverseMap();

            CreateMap<Skill, SkillEditDTO>().ReverseMap();

            CreateMap<Skill, SkillReadDTO>().ReverseMap();
        }
    }
}
