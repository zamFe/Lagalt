namespace LagaltAPI.Models.DTOs.User
{
    public class UserEditDTO
    {
        public int Id { get; set; }
        public int[] Skills { get; set; }
        public string Username { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Portfolio { get; set; }
    }
}
