namespace LagaltAPI.Models.DTOs.Project
{
    public class ProjectCreateDTO
    {
        public int ProfessionId { get; set; }
        public int[] UserIds { get; set; }
        public int[] SkillIds { get; set; }
        public int[] AdministratorIds { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Progress { get; set; }
        public string Image { get; set; }
        public string Source { get; set; }
    }
}
