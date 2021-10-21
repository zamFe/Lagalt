namespace LagaltAPI.Models.DTOs.User
{
    /// <summary> A creation-specific DTO representing a new user. </summary>
    public class UserCreateDTO
    {
        /// <summary> The ids of the skills which the user posesses. </summary>
        public int[] Skills { get; set; }

        /// <summary> The name the user is referred to as. </summary>
        public string Username { get; set; }

        /// <summary> The user's description of themselves. </summary>
        public string Description { get; set; }

        /// <summary> A URI to an image used as the user's avatar. </summary>
        public string Image { get; set; }

        /// <summary> A URI to an portfolio showcasing the user's accomplishment. </summary>
        public string Portfolio { get; set; }
    }
}
