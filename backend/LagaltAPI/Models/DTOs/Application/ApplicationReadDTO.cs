using LagaltAPI.Models.DTOs.User;

namespace LagaltAPI.Models.DTOs.Application
{
    /// <summary> A read-specific DTO of an application. </summary>
    public class ApplicationReadDTO
    {
        /// <summary> The application's id. </summary>
        public int Id { get; set; }

        /// <summary> The user who submitted the application. </summary>
        public UserCompleteReadDTO User { get; set; }

        /// <summary> Whether the application has been accepted or not. </summary>
        public bool Accepted { get; set; }

        /// <summary> Whether the application has been seen or not. </summary>
        public bool Seen { get; set; }

        /// <summary>
        ///     A short motivational text written by the user who submitted the application.
        /// </summary>
        public string Motivation { get; set; }
    }
}
