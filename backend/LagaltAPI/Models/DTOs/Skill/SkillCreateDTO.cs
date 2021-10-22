namespace LagaltAPI.Models.DTOs.Skill
{
    /// <summary> A creation-specific DTO representing a new skill. </summary>
    public class SkillCreateDTO
    {
        /// <summary> The name of the skill. </summary>
        public string Name { get; set; }

        /// <summary> The ids of the users who will be marked as having the skill. </summary>
        public int[] Users { get; set; }

        /// <summary>
        ///     The ids of the projects who will be marked as being interested in the skill.
        ///     </summary>
        public int[] Projects { get; set; }
    }
}
