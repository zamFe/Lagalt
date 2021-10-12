using AutoMapper;
using LagaltAPI.Models.Domain;
using LagaltAPI.Models.DTOs.Message;

namespace LagaltAPI.Profiles
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageCreateDTO>().ReverseMap();

            CreateMap<Message, MessageReadDTO>().ReverseMap();
        }
    }
}
