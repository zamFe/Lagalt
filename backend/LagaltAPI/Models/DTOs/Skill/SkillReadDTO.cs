namespace LagaltAPI.Models.DTOs.Skill
{
    /// <summary> A read-specific DTO of a skill. </summary>
    public class SkillReadDTO
    {
        /// <summary> The skill's id. </summary>
        public int Id { get; set; }

        /// <summary> The name of the skill. </summary>
        public string Name { get; set; }
    }
}
