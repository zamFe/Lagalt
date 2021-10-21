namespace LagaltAPI.Models.DTOs.Application
{
    /// <summary> An edit-specific DTO representing changes to make to an application. </summary>
    public class ApplicationEditDTO
    {
        /// <summary> The application's id. </summary>
        public int Id { get; set; }

        /// <summary> Whether the application has been accepted or not. </summary>
        public bool Accepted { get; set; }

        /// <summary> Whether the application has been seen or not. </summary>
        public bool Seen { get; set; }
    }
}
