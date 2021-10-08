namespace LagaltAPI.Models.DTOs.User
{
    public class UserCreateDTO
    {
        public string UserName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Portfolio { get; set; }
        public int[] Skills { get; set; }
    }
}
