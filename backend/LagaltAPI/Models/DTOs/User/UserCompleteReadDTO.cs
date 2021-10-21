using LagaltAPI.Models.DTOs.Project;
using LagaltAPI.Models.DTOs.Skill;

namespace LagaltAPI.Models.DTOs.User
{
    /// <summary> A larger, read-specific DTO of a user. </summary>
    public class UserCompleteReadDTO
    {
        /// <summary> The user's id. </summary>
        public int Id { get; set; }

        /// <summary> The projects joined by the user. </summary>
        public ProjectCompactReadDTO[] Projects { get; set; }

        /// <summary> The skills which the user posesses. </summary>
        public SkillReadDTO[] Skills { get; set; }

        /// <summary>
        ///     Whether the user wants to hide their projects, skills, description and portfolio.
        /// </summary>
        public bool Hidden { get; set; }

        /// <summary> The name the user is referred to as. </summary>
        public string Username { get; set; }

        /// <summary> The user's description of themselves. </summary>
        public string Description { get; set; }

        /// <summary> A URI to an image used as the user's avatar. </summary>
        public string Image { get; set; }

        /// <summary> A URI to an portfolio showcasing the user's accomplishment. </summary>
        public string Portfolio { get; set; }
    }
}
