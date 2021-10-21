using AutoMapper;
using LagaltAPI.Models.Domain;
using LagaltAPI.Models.DTOs.Message;

namespace LagaltAPI.Profiles
{
    public class MessageProfile : Profile
    {
        // Constructor.
        public MessageProfile()
        {
            CreateMap<Message, MessageReadDTO>();

            CreateMap<MessageCreateDTO, Message>();
        }
    }
}
