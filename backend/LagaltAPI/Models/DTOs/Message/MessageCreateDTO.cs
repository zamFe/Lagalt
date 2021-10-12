using System;

namespace LagaltAPI.Models.DTOs.Message
{
    public class MessageCreateDTO
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public string Content { get; set; }
        public DateTime PostedTime { get; set; }
    }
}
