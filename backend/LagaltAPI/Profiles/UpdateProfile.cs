using AutoMapper;
using LagaltAPI.Models.Domain;
using LagaltAPI.Models.DTOs.Update;

namespace LagaltAPI.Profiles
{
    public class UpdateProfile : Profile
    {
        // Constructor.
        public UpdateProfile()
        {
            CreateMap<Update, UpdateReadDTO>();

            CreateMap<UpdateCreateDTO, Update>();
        }
    }
}
