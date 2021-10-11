namespace LagaltAPI.Models.DTOs.User
{
    public class UserReadDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Portfolio { get; set; }
        public int[] Skills { get; set; }
        public int [] Projects { get; set; }
    }
}
