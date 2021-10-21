using LagaltAPI.Models.DTOs.User;
using System;

namespace LagaltAPI.Models.DTOs.Update
{
    /// <summary> A read-specific DTO of a project update. </summary>
    public class UpdateReadDTO
    {
        /// <summary> The project update's id. </summary>
        public int Id { get; set; }

        /// <summary> The user who posted the update. </summary>
        public UserCompactReadDTO User { get; set; }

        /// <summary> The actual project update message. </summary>
        public string Content { get; set; }

        /// <summary> The time at which the update was posted. </summary>
        public DateTime PostedTime { get; set; }
    }
}
