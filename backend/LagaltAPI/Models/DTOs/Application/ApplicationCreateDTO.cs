namespace LagaltAPI.Models.DTOs.Application
{
    /// <summary> A creation-specific DTO representing a new application. </summary>
    public class ApplicationCreateDTO
    {
        /// <summary> The id of the project which the application was submitted to. </summary>
        public int ProjectId { get; set; }

        /// <summary> The id of the user who submitted the application. </summary>
        public int UserId { get; set; }

        /// <summary>
        ///     A short motivational text written by the user who submitted the application.
        /// </summary>
        public string Motivation { get; set; }
    }
}
