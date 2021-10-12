using System;

namespace LagaltAPI.Models.DTOs.Message
{
    public class MessageReadDTO
    {
        // TODO - Embed a UserReadDTO instead of using a user id.
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime PostedTime { get; set; }
    }
}
