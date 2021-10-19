using LagaltAPI.Models.DTOs.Profession;
using LagaltAPI.Models.DTOs.Skill;
using LagaltAPI.Models.DTOs.User;

namespace LagaltAPI.Models.DTOs.Project
{
    public class ProjectCompleteReadDTO
    {
        public int Id { get; set; }
        public ProfessionReadDTO Profession { get; set; }
        public SkillReadDTO[] Skills { get; set; }
        public UserCompactReadDTO[] Users { get; set; }
        public int[] AdministratorIds { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Progress { get; set; }
        public string Image { get; set; }
        public string Source { get; set; }
    }
}
