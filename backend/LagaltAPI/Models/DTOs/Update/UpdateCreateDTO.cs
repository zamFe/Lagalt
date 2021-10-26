using System;

namespace LagaltAPI.Models.DTOs.Update
{
    /// <summary> A creation-specific DTO representing a new project update. </summary>
    public class UpdateCreateDTO
    {
        /// <summary> The id of the project which the update was submitted to. </summary>
        public int ProjectId { get; set; }

        /// <summary> The id of the user who posted the update. </summary>
        public int UserId { get; set; }

        /// <summary> The actual project update message. </summary>
        public string Content { get; set; }

        /// <summary> The time at which the update was posted. </summary>
        public DateTime PostedTime { get; set; }
    }
}
