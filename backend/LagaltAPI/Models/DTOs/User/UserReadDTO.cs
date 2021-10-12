using LagaltAPI.Models.DTOs.Skill;

namespace LagaltAPI.Models.DTOs.User
{
    public class UserReadDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Portfolio { get; set; }
        public SkillReadDTO[] Skills { get; set; }
        public int[] Projects { get; set; }
    }
}
