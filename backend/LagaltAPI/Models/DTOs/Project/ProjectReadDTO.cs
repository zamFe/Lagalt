using LagaltAPI.Models.DTOs.Skill;

namespace LagaltAPI.Models.DTOs.Project
{
    public class ProjectReadDTO
    {
        public int Id { get; set; }
        public int Profession { get; set; }
        public int[] Users { get; set; }
        public int[] Administrators { get; set; }
        public SkillReadDTO[] Skills { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Progress { get; set; }
        public string Image { get; set; }
        public string Source { get; set; }
    }
}
