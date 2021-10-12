using AutoMapper;
using LagaltAPI.Models.Domain;
using LagaltAPI.Models.DTOs.Message;

namespace LagaltAPI.Profiles
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageCreateDTO>()
                .ForMember(mdto => mdto.User, opt => opt
                .MapFrom(m => m.UserId))
                .ForMember(mdto => mdto.Project, opt => opt
                .MapFrom(m => m.ProjectId))
                .ReverseMap();

            CreateMap<Message, MessageReadDTO>()
                .ForMember(mdto => mdto.User, opt => opt
                .MapFrom(m => m.UserId))
                .ReverseMap();
        }
    }
}
