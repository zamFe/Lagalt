using System;

namespace LagaltAPI.Models.DTOs.Message
{
    /// <summary> A creation-specific DTO representing a new application. </summary>
    public class MessageCreateDTO
    {
        /// <summary> The id of the user who posted the message. </summary>
        public int UserId { get; set; }

        /// <summary> The id of the project which the message was posted to. </summary>
        public int ProjectId { get; set; }

        /// <summary> The contents of the message. </summary>
        public string Content { get; set; }

        /// <summary> The time at which the message was posted. </summary>
        public DateTime PostedTime { get; set; }
    }
}
