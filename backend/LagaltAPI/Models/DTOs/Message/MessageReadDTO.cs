using LagaltAPI.Models.DTOs.User;
using System;

namespace LagaltAPI.Models.DTOs.Message
{
    /// <summary> A read-specific DTO of a project message. </summary>
    public class MessageReadDTO
    {
        /// <summary> The message's id. </summary>
        public int Id { get; set; }

        /// <summary> The user who wrote the message. </summary>
        public UserCompactReadDTO User { get; set; }

        /// <summary> The contents of the message. </summary>
        public string Content { get; set; }

        /// <summary> The time at which the message was posted. </summary>
        public DateTime PostedTime { get; set; }
    }
}
