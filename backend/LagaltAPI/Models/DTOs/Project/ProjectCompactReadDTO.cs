using LagaltAPI.Models.DTOs.Profession;
using LagaltAPI.Models.DTOs.Skill;

namespace LagaltAPI.Models.DTOs.Project
{
    /// <summary> A smaller, read-specific DTO of a project. </summary>
    public class ProjectCompactReadDTO
    {
        /// <summary> The project's id. </summary>
        public int Id { get; set; }

        /// <summary> The profession which the project falls under. </summary>
        public ProfessionReadDTO Profession { get; set; }

        /// <summary> The skills which the project is interested in. </summary>
        public SkillReadDTO[] Skills { get; set; }

        /// <summary> The title of the project. </summary>
        public string Title { get; set; }

        /// <summary> A description of the project. </summary>
        public string Description { get; set; }

        /// <summary> The curent progress status of the project. </summary>
        public string Progress { get; set; }

        /// <summary> A URI to an image used to represent the project. </summary>
        public string Image { get; set; }
    }
}
