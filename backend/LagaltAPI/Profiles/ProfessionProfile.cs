using AutoMapper;
using LagaltAPI.Models.Domain;
using LagaltAPI.Models.DTOs.Profession;

namespace LagaltAPI.Profiles
{
    public class ProfessionProfile : Profile
    {
        // Constructor.
        public ProfessionProfile()
        {
            CreateMap<Profession, ProfessionReadDTO>();
        }
    }
}
