using System;

namespace LagaltAPI.Models.DTOs.Message
{
    public class MessageCreateDTO
    {
        public int User { get; set; }
        public int Project { get; set; }
        public string Content { get; set; }
        public DateTime PostedTime { get; set; }
    }
}
