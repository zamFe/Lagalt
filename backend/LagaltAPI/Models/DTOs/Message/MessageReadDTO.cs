using LagaltAPI.Models.DTOs.User;
using System;

namespace LagaltAPI.Models.DTOs.Message
{
    public class MessageReadDTO
    {
        public int Id { get; set; }
        public UserCompactReadDTO User { get; set; }
        public string Content { get; set; }
        public DateTime PostedTime { get; set; }
    }
}
