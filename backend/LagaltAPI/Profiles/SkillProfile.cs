using AutoMapper;
using LagaltAPI.Models.Domain;
using LagaltAPI.Models.DTOs.Skill;

namespace LagaltAPI.Profiles
{
    public class SkillProfile : Profile
    {
        // Constructor.
        public SkillProfile()
        {
            CreateMap<Skill, SkillEditDTO>();
            CreateMap<Skill, SkillReadDTO>();

            CreateMap<SkillCreateDTO, Skill>()
                .ForMember(sdto => sdto.Users, opt => opt
                .Ignore())
                .ForMember(sdto => sdto.Projects, opt => opt
                .Ignore());
        }
    }
}
