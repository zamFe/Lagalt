using System;

namespace LagaltAPI.Models.DTOs.Message
{
    public class MessageEditDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime PostedTime { get; set; }
    }
}
