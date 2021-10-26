namespace LagaltAPI.Models.DTOs.Project
{
    /// <summary> A creation-specific DTO representing a new project. </summary>
    public class ProjectCreateDTO
    {
        /// <summary> The id of the profession which the project falls under. </summary>
        public int ProfessionId { get; set; }

        /// <summary> The ids of the users to include in the project. </summary>
        public int[] Users { get; set; }

        /// <summary> The ids of the skills which the project is interested in. </summary>
        public int[] Skills { get; set; }

        /// <summary> The ids of the users to include in the project. </summary>
        public int[] AdministratorIds { get; set; }

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
