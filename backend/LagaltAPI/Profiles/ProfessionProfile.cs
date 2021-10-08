using AutoMapper;
using LagaltAPI.Models;
using LagaltAPI.Models.DTOs.Profession;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LagaltAPI.Profiles
{
    public class ProfessionProfile : Profile
    {
        public ProfessionProfile()
        {
            CreateMap<Profession, ProfessionReadDTO>()
                .ForMember(pdto => pdto.Projects, opt => opt
                .MapFrom(p => p.Projects.Select(proj => proj.Id).ToList()))
                .ReverseMap();
        }
    }
}
