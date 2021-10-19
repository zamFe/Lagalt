using LagaltAPI.Models.DTOs.Profession;
using LagaltAPI.Models.DTOs.Skill;

namespace LagaltAPI.Models.DTOs.Project
{
    public class ProjectCompactReadDTO
    {
        public int Id { get; set; }
        public ProfessionReadDTO Profession { get; set; }
        public SkillReadDTO[] Skills { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Progress { get; set; }
        public string Image { get; set; }
    }
}
