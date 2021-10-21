namespace LagaltAPI.Models.DTOs.User
{
    /// <summary> A smaller, read-specific DTO of a project. </summary>
    public class UserCompactReadDTO
    {
        /// <summary> The user's id. </summary>
        public int Id { get; set; }

        /// <summary> The name the user is referred to as. </summary>
        public string Username { get; set; }

        /// <summary> A URI to an image used as the user's avatar. </summary>
        public string Image { get; set; }
    }
}
