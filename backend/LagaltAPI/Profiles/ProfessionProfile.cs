using AutoMapper;
using LagaltAPI.Models;
using LagaltAPI.Models.DTOs.Profession;

namespace LagaltAPI.Profiles
{
    public class ProfessionProfile : Profile
    {
        public ProfessionProfile()
        {
            CreateMap<Profession, ProfessionReadDTO>().ReverseMap();
        }
    }
}
