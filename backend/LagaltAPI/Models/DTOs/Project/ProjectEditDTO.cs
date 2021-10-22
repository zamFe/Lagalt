namespace LagaltAPI.Models.DTOs.Project
{
    /// <summary> An edit-specific DTO representing changes to make to a project. </summary>
    public class ProjectEditDTO
    {
        /// <summary> The project's id. </summary>
        public int Id { get; set; }

        /// <summary> The ids of the skills which the project is interested in. </summary>
        public int[] Skills { get; set; }

        /// <summary> The title of the project. </summary>
        public string Title { get; set; }

        /// <summary> A description of the project. </summary>
        public string Description { get; set; }

        /// <summary> The curent progress status of the project. </summary>
        public string Progress { get; set; }

        /// <summary> A URI to an image used to represent the project. </summary>
        public string Image { get; set; }

        /// <summary> A URI to a site containing the artefacts produced by the project. </summary>
        public string Source { get; set; }
        
    }
}
