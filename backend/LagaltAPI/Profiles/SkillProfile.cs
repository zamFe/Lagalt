using AutoMapper;
using LagaltAPI.Models.Domain;
using LagaltAPI.Models.DTOs.Skill;
using System.Linq;

namespace LagaltAPI.Profiles
{
    public class SkillProfile : Profile
    {
        public SkillProfile()
        {
            CreateMap<SkillCreateDTO, Skill>()
                .ForMember(sdto => sdto.Users, opt => opt
                .Ignore())
                .ForMember(sdto => sdto.Projects, opt => opt
                .Ignore());

            CreateMap<Skill, SkillEditDTO>().ReverseMap();

            CreateMap<Skill, SkillReadDTO>().ReverseMap();
        }
    }
}
