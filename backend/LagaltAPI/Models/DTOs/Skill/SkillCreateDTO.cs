namespace LagaltAPI.Models.DTOs.Skill
{
    public class SkillCreateDTO
    {
        public string Name { get; set; }
        public int[] Users { get; set; }
        public int[] Projects { get; set; }
    }
}
